using AutoMapper;
using Moq;
using WorkshopBooking.Application.Features.ServiceTypeFeature.Commands.CreateServiceType;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Tests.CreateServiceTypeCommandHandlerTests
{
    public class CreateServiceTypeCommandHandlerTests
    {
        private CreateServiceTypeCommandHandler _handler;
        private Mock<IGenericInterface<ServiceType>> _mockRepository;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IGenericInterface<ServiceType>>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateServiceTypeCommandHandler(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Handle_ValidInput_ReturnsSuccess()
        {
            // Arrange
            var inputDto = new ServiceTypeInputDto
            {
                Name = "Test Service",
                Price = 100,
                Duration = TimeSpan.FromMinutes(60)
            };

            var serviceType = new ServiceType
            {
                ServiceTypeId = 1,
                Name = inputDto.Name,
                Price = inputDto.Price,
                Duration = inputDto.Duration
            };

            var outputDto = new ServiceTypeDto
            {
                ServiceTypeId = 1,
                Name = inputDto.Name,
                Price = inputDto.Price,
                Duration = inputDto.Duration
            };

            var command = new CreateServiceTypeCommand(inputDto);

            _mockMapper.Setup(m => m.Map<ServiceType>(inputDto)).Returns(serviceType);
            _mockRepository.Setup(r => r.AddAsync(serviceType)).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<ServiceTypeDto>(serviceType)).Returns(outputDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(outputDto.ServiceTypeId, result.Data.ServiceTypeId);
        }

        [Test]
        public async Task Handle_ThrowsException_ReturnsFailure()
        {
            // Arrange
            var inputDto = new ServiceTypeInputDto
            {
                Name = "Fail Service",
                Price = 200,
                Duration = TimeSpan.FromMinutes(45)
            };

            var command = new CreateServiceTypeCommand(inputDto);

            _mockMapper.Setup(m => m.Map<ServiceType>(inputDto)).Throws(new Exception("Mapping error"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNull(result.Data);
            Assert.That(result.ErrorMessage, Does.Contain("An error occurred while creating service type"));
        }
    }
}
