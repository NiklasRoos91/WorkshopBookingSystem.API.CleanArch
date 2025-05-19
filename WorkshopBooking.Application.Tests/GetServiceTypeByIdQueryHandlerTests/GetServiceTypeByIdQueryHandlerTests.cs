using AutoMapper;
using WorkshopBooking.Application.Features.ServiceTypeFeature.Handlers.Queries;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Application.Features.ServiceTypeFeature.DTOs;
using WorkshopBooking.Application.Features.ServiceTypeFeature.Queries;
using WorkshopBooking.Application.Commons.OperationResult;
using Moq;
using WorkshopBooking.Domain.Interfaces;


namespace WorkshopBooking.Application.Tests.GetServiceTypeByIdQueryHandlerTests
{
    public class Tests
    {
        [TestFixture]
        public class GetServiceTypeByIdQueryHandlerTests
        {
            private GetServiceTypeByIdQueryHandler _handler;
            private Mock<IGenericInterface<ServiceType>> _mockRepository;
            private Mock<IMapper> _mockMapper;

            [SetUp]
            public void Setup()
            {
                _mockRepository = new Mock<IGenericInterface<ServiceType>>();
                _mockMapper = new Mock<IMapper>();
                _handler = new GetServiceTypeByIdQueryHandler(_mockRepository.Object, _mockMapper.Object);
            }

            [Test]
            public async Task Handle_ValidId_ReturnsSuccessResult()
            {
                // Arrange (förberedelser)
                var serviceTypeId = 1; // Sätter ett giltigt serviceTypeId som vi testar mot
                var serviceType = new ServiceType { ServiceTypeId = serviceTypeId, Name = "Test ServiceType" }; // Skapar ett mockat ServiceType-objekt med det giltiga ID:t
                var serviceTypeDto = new ServiceTypeDto { ServiceTypeId = serviceTypeId, Name = "Test ServiceType" }; // Skapar ett mockat DTO-objekt med samma data

                var query = new GetServiceTypeByIdQuery(serviceTypeId); // Skapar en Query med ID:t för att skicka till handlern

                // Mockar repository så att när GetByIdAsync kallas med serviceTypeId, returneras mockade serviceType-objektet
                _mockRepository.Setup(r => r.GetByIdAsync(serviceTypeId))
                    .ReturnsAsync(serviceType);

                // Mockar mapper så att när Map anropas med serviceType, returneras serviceTypeDto
                _mockMapper.Setup(m => m.Map<ServiceTypeDto>(serviceType))
                    .Returns(serviceTypeDto);

                // Act (utförande)
                var result = await _handler.Handle(query, CancellationToken.None); // Kör handlerns Handle-metod med vår query

                // Assert (verifiering)
                Assert.IsTrue(result.IsSuccess); // Verifierar att operationen var lyckad
                Assert.IsNotNull(result.Data); // Verifierar att data inte är null
                Assert.AreEqual(serviceTypeId, result.Data.ServiceTypeId); // Verifierar att ID:t i resultatet matchar
                Assert.AreEqual(serviceTypeDto.Name, result.Data.Name); // Verifierar att namnet i resultatet matchar DTO:ns namn
            }

            [Test]
            public async Task Handle_InvalidId_ReturnsFailureResult()
            {
                // Arrange
                var invalidId = 999; // Sätter ett ogiltigt ID för att testa felhantering
                var query = new GetServiceTypeByIdQuery(invalidId); // Skapar en Query med det ogiltiga ID:t

                // Mockar repository så att när GetByIdAsync kallas med invalidId, returneras null
                _mockRepository.Setup(r => r.GetByIdAsync(invalidId))
            .ReturnsAsync((ServiceType?)null);

                // Act
                var result = await _handler.Handle(query, CancellationToken.None); // Kör handlerns Handle-metod med vår query

                // Assert
                Assert.IsFalse(result.IsSuccess); // Verifierar att operationen misslyckades
                Assert.IsNull(result.Data); // Verifierar att data är null
                Assert.That(result.ErrorMessage, Is.EqualTo("Service type not found.")); // Verifierar att felmeddelandet är korrekt
            }
        }
    }
}