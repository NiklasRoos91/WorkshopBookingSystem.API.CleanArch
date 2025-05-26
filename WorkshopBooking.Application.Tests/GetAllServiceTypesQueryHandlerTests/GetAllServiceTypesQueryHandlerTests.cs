using AutoMapper;
using Moq;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;
using WorkshopBooking.Application.Features.ServiceTypeFeature.Queries.GetAllServiceTypes;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Tests
{
    [TestFixture]
    public class GetAllServiceTypesQueryHandlerTests
    {
        private GetAllServiceTypesQueryHandler _handler;
        private Mock<IGenericInterface<ServiceType>> _mockRepository;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IGenericInterface<ServiceType>>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetAllServiceTypesQueryHandler(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Handle_WhenServiceTypesExist_ReturnsListOfServiceTypeDtos()
        {
            // Arrange
            // Mock data
            var serviceTypes = new List<ServiceType>
            {
                new ServiceType { ServiceTypeId = 1, Name = "Oil Change" },
                new ServiceType { ServiceTypeId = 2, Name = "Tire Rotation" }
            };

            var serviceTypeDtos = new List<ServiceTypeDto>
            {
                new ServiceTypeDto { ServiceTypeId = 1, Name = "Oil Change" },
                new ServiceTypeDto { ServiceTypeId = 2, Name = "Tire Rotation" }
            };


            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(serviceTypes); // Mocking the repository method
            _mockMapper.Setup(m => m.Map<List<ServiceTypeDto>>(serviceTypes)).Returns(serviceTypeDtos); // Mocking the mapper

            var query = new GetAllServiceTypesQuery(); // Create the query object

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);  // Call the handler method

            // Assert
            Assert.IsTrue(result.IsSuccess); // Check if the result is successful
            Assert.AreEqual(2, result.Data.Count); // Check the count of service types
            Assert.AreEqual("Oil Change", result.Data[0].Name); // Check the first service type
        }

        [Test]
        public async Task Handle_WhenExceptionThrown_ReturnsFailure()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database failed")); // Simulate an exception

            var query = new GetAllServiceTypesQuery(); // Create the query object

            // Act
            var result = await _handler.Handle(query, CancellationToken.None); // Call the handler method

            // Assert
            Assert.IsFalse(result.IsSuccess); // Check if the result is not successful
            Assert.IsNull(result.Data); // Check if the data is null
            Assert.That(result.ErrorMessage, Does.Contain("An error occurred while retrieving service types")); // Check the error message
        }
    }
}
