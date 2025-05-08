using AutoMapper;
using MediatR;
using WorkshopBooking.Application.Commons.OperationResult;
using WorkshopBooking.Application.Features.CustomerFeature.Commands;
using WorkshopBooking.Application.Features.CustomerFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Features.CustomerFeature.Handlers.Commands
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, OperationResult<CustomerDto>>
    {
        private readonly IGenericInterface<Customer> _customerRepository;
        private readonly IMapper _mapper;
        public UpdateCustomerCommandHandler(IGenericInterface<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<CustomerDto>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existinCcustomer = await _customerRepository.GetByIdAsync(request.CustomerId);
                if (existinCcustomer == null)
                {
                    return OperationResult<CustomerDto>.Failure("Customer not found.");
                }

                // Map the updated properties from CustomerInputDto to the existing customer entity
                _mapper.Map(request.CustomerInputDto, existinCcustomer);

                // Update the customer in the repository
                await _customerRepository.UpdateAsync(existinCcustomer);

                // Map the updated customer entity to CustomerDto
                var customerDto = _mapper.Map<CustomerDto>(existinCcustomer);

                // Return the result
                return OperationResult<CustomerDto>.Success(customerDto);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the update process
                return OperationResult<CustomerDto>.Failure($"An error occurred while updating customer: {ex.Message}");
            }
        }
    }
}
