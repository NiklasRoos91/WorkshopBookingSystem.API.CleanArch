using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.BookingFeature.Queries.GetBookingByIdQuery
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, OperationResult<BookingDto>>
    {
        private readonly IGenericInterface<Booking> _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetBookingByIdQueryHandler(IGenericInterface<
            Booking> bookingRepository,
            ICustomerRepository customerRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<BookingDto>> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var booking = await _bookingRepository.GetByIdAsync(request.BookingId);
                if (booking == null)
                {
                    return OperationResult<BookingDto>.Failure("Booking not found.");
                }

                if (!request.IsAdmin)
                {
                    bool hasAccess = false;

                    // Kontrollera Customer
                    var existingCustomer = await _customerRepository.GetCustomerWithUserByCustomerIdAsync(booking.CustomerId);
                    if (existingCustomer != null && existingCustomer.UserId == request.UserId)
                    {
                        hasAccess = true;
                    }

                    // Kontrollera Employee
                    var existingEmployee = await _employeeRepository.GetEmployeeWithUserByEmployeeIdAsync(booking.EmployeeId);
                    if (existingEmployee != null && existingEmployee.UserId == request.UserId)
                    {
                        hasAccess = true;
                    }

                    if (!hasAccess)
                    {
                        return OperationResult<BookingDto>.Failure("You do not have permission to view this booking.");
                    }
                }

                var bookingDto = _mapper.Map<BookingDto>(booking);

                return OperationResult<BookingDto>.Success(bookingDto);
            }
            catch (Exception ex)
            {
                return OperationResult<BookingDto>.Failure($"An error occurred while retrieving the booking: {ex.Message}");
            }
        }
    }
}
