using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.Commands;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.CustomerFeature.Handlers.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, OperationResult<CustomerDto>>
    {
        private readonly IGenericInterface<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(IGenericInterface<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<OperationResult<CustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = _mapper.Map<Customer>(request.CustomerInputDto);

                await _customerRepository.AddAsync(customer);

                var customerDto = _mapper.Map<CustomerDto>(customer);

                return OperationResult<CustomerDto>.Success(customerDto);
            }
            catch (Exception ex)
            {
                return OperationResult<CustomerDto>.Failure($"An error occurred while creating customer: {ex.Message}");
            }
        }
    }
}
