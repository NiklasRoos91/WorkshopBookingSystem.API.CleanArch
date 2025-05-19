using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopBooking.Application.Features.ServiceTypeFeature.Commands;
using WorkshopBooking.Application.Features.ServiceTypeFeature.Handlers.Commands;
using WorkshopBooking.Domain.Entities;
using WorkshopBooking.Domain.Interfaces;

namespace WorkshopBooking.Application.Tests.DeleteServiceTypeCommandHandlerTests
{
    [TestFixture]
    public class DeleteServiceTypeCommandHandlerTests
    {
        private DeleteServiceTypeCommandHandler _handler;
        private Mock<IGenericInterface<ServiceType>> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IGenericInterface<ServiceType>>();
            _handler = new DeleteServiceTypeCommandHandler(_mockRepository.Object);
        }

        [Test]
        public async Task Handle_ExistingServiceTypeId_DeletesSuccessfully()
        {
            // Arrange
            var serviceTypeId = 15; // Exempel ID
            var serviceType = new ServiceType { ServiceTypeId = serviceTypeId, Name = "Test ServiceType" }; // Mockad ServiceType
            var command = new DeleteServiceTypeCommand(serviceTypeId); // Skapa kommandot med ID

            // Mockar att GetByIdAsync hittar ServiceType
            _mockRepository.Setup(r => r.GetByIdAsync(serviceTypeId))
                .ReturnsAsync(serviceType);

            // Mockar att DeleteAsync lyckas (returnerar true eller task)
            _mockRepository.Setup(r => r.DeleteAsync(serviceTypeId))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None); // Anropa Handle-metoden med kommandot

            // Assert
            Assert.IsTrue(result.IsSuccess); // Indikerar att operationen lyckades
            Assert.IsTrue(result.Data); // Indikerar att borttagning lyckades
            Assert.IsNull(result.ErrorMessage); // Inga felmeddelanden ska returneras
        }

        [Test]
        public async Task Handle_NonExistingServiceTypeId_ReturnsFailure()
        {
            // Arrange
            var serviceTypeId = 74; // Exempel ID som inte finns
            var command = new DeleteServiceTypeCommand(serviceTypeId);  // Skapa kommandot med ID

            // Mockar att GetByIdAsync inte hittar ServiceType (returnerar null)
            _mockRepository.Setup(r => r.GetByIdAsync(serviceTypeId))
                .ReturnsAsync((ServiceType?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None); // Anropa Handle-metoden med kommandot

            // Assert
            Assert.IsFalse(result.IsSuccess); // Indikerar att operationen misslyckades
            Assert.IsFalse(result.Data); // Borttagning skedde inte
            Assert.That(result.ErrorMessage, Is.EqualTo($"ServiceType with ID {serviceTypeId} not found.")); // Felmeddelande ska indikera att ServiceType inte hittades
        }
    }

}
