using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.BookingFeature.Queries.GetBookingsByEmployeeIdQuery
{
    public class GetBookingsByEmployeeIdQueryHandler : IRequestHandler<GetBookingsByEmployeeIdQuery, OperationResult<List<BookingDto>>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetBookingsByEmployeeIdQueryHandler(
            IBookingRepository bookingRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<BookingDto>>> Handle(GetBookingsByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                int employeeIdToUse = request.EmployeeId;

                if (!request.IsAdmin)
                {
                    var employee = await _employeeRepository.GetEmployeeWithUserByUserIdAsync(request.UserId);
                    if (employee == null)
                    {
                        return OperationResult<List<BookingDto>>.Failure("Employee not found.");
                    }
                    employeeIdToUse = employee.EmployeeId;
                }

                var bookings = await _bookingRepository.GetBookingsByEmployeeIdAsync(employeeIdToUse);
                if (bookings == null || bookings.Count == 0)
                {
                    return OperationResult<List<BookingDto>>.Failure("No bookings found for the specified employee.");
                }

                var bookingDtos = _mapper.Map<List<BookingDto>>(bookings);

                return OperationResult<List<BookingDto>>.Success(bookingDtos);
            }
            catch (Exception ex)
            {
                return OperationResult<List<BookingDto>>.Failure($"An error occurred while retrieving bookings: {ex.Message}");
            }
        }
    }
}