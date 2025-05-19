using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.BookingFeature.Commands.CreateBookingFromSlotCommand
{
    public class CreateBookingFromSlotCommandHandler : IRequestHandler<CreateBookingFromSlotCommand, OperationResult<BookingDto>>
    {
        private readonly IGenericInterface<Booking> _bookingRepository;
        private readonly IGenericInterface<AvailableSlot> _availableSlotRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateBookingFromSlotCommandHandler(
            IGenericInterface<Booking> bookingRepository,
            IGenericInterface<AvailableSlot> availableSlotRepository,
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _availableSlotRepository = availableSlotRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<BookingDto>> Handle(CreateBookingFromSlotCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerWithUserByUserIdAsync(request.UserId);
                if (customer == null)
                {
                    return OperationResult<BookingDto>.Failure("Customer not found.");
                }

                var slotId = request.CreateBookingFromSlotDto.AvailableSlotId;

                var slot = await _availableSlotRepository.GetByIdAsync(slotId);
                if (slot == null || !slot.IsAvailable)
                {
                    return OperationResult<BookingDto>.Failure("Slot not available.");
                }

                if (slot.StartTime < DateTime.UtcNow)
                {
                    return OperationResult<BookingDto>.Failure("Slot has already passed.");
                }

                var booking = _mapper.Map<Booking>(slot);
                booking.CustomerId = customer.CustomerId;

                await _bookingRepository.AddAsync(booking);

                slot.IsAvailable = false;
                await _availableSlotRepository.UpdateAsync(slot);

                var bookingDto = _mapper.Map<BookingDto>(booking);
                return OperationResult<BookingDto>.Success(bookingDto);
            }
            catch (Exception ex)
            {
                return OperationResult<BookingDto>.Failure($"An error occurred while creating the booking: {ex.Message}");
            }
        }
    }
}