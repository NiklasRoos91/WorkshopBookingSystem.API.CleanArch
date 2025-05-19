using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.BookingFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.BookingFeature.Queries.GetAllBookingsByQuery
{
    public class GetAllBookingsByQueryHandler : IRequestHandler<GetAllBookingsByQuery, OperationResult<List<BookingDto>>>
    {
        private readonly IGenericInterface<Booking> _bookingRepository;
        private readonly IMapper _mapper;

        public GetAllBookingsByQueryHandler(IGenericInterface<Booking> bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }
        public async Task<OperationResult<List<BookingDto>>> Handle(GetAllBookingsByQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookings = _bookingRepository.GetAllAsync().Result;
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
