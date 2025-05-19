using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.BookingFeature.Commands.CreateBookingCommand
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, OperationResult<BookingDto>>
    {
        private readonly IGenericInterface<Booking> _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public CreateBookingCommandHandler(
            IGenericInterface<Booking> bookingRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }


        public async Task<OperationResult<BookingDto>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeWithUserByUserIdAsync(request.UserId);
                if (employee == null)
                {
                    return OperationResult<BookingDto>.Failure("The user does not have a linked Employee.");
                }


                var booking = _mapper.Map<Booking>(request.CreateBookingDto);
                booking.EmployeeId = employee.EmployeeId;

                await _bookingRepository.AddAsync(booking);

                var dto = _mapper.Map<BookingDto>(booking);
                return OperationResult<BookingDto>.Success(dto);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : "";

                return OperationResult<BookingDto>.Failure($"An error occurred while creating the booking: {ex.Message}");
            }
        }
    }
}
