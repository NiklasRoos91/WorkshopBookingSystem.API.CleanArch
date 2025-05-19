using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.AvailableSlotFeature.DTOs;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.BookingFeature.Commands.UpdateBookingCommand
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, OperationResult<BookingDto>>
    {
        private readonly IGenericInterface<Booking> _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UpdateBookingCommandHandler(
            IGenericInterface<Booking> bookingRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<BookingDto>> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeWithUserByUserIdAsync(request.UserId);
                if (employee == null)
                {
                    return OperationResult<BookingDto>.Failure("The user does not have a linked Employee.");
                }

                var booking = await _bookingRepository.GetByIdAsync(request.BookingId);
                if (booking == null)
                {
                    return OperationResult<BookingDto>.Failure("Booking not found.");
                }

                if (booking.EmployeeId != employee.EmployeeId)
                {
                    return OperationResult<BookingDto>.Failure("You are not authorized to update this booking.");
                }

                booking.StartTime = request.UpdateBookingDto.StartTime;
                booking.EndTime = request.UpdateBookingDto.EndTime;
                booking.ServiceTypeId = request.UpdateBookingDto.ServiceTypeId;

                await _bookingRepository.UpdateAsync(booking);

                var dto = _mapper.Map<BookingDto>(booking);
                return OperationResult<BookingDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return OperationResult<BookingDto>.Failure($"An error occurred while updating the booking: {ex.Message}");
            }
        }
    }
}