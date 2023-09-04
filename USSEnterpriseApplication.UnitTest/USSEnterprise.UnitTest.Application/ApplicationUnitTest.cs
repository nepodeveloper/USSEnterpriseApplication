using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USSEnterprise.Application.Interfaces;
using USSEnterprise.Application.Services;
using USSEnterprise.Domain.Entities;
using USSEnterpriseApplication.Infrastructure.Data.Interfaces;
using USSEnterpriseApplication.Infrastructure.Data.Repositories;

namespace USSEnterpriseApplication.UnitTest.USSEnterprise.UnitTest.Application
{
    [TestClass]
    public class ApplicationUnitTest
    {

        IServiceProvider serviceProvider = ConfigureServices();
        int elevatorId = 1;

        [TestMethod]
        public void RequestFloor_ShouldRejectInvalidFloorNumber()
        {

            // Arrange
            var elevatorService = new ElevatorService(new Elevator(), 10);

            // Act
            var exception =  Assert.ThrowsException<ArgumentException>(() => elevatorService.RequestFloor(-1));

            // Assert
            Assert.IsTrue(exception.Message.Contains("Invalid floor request"));
        }

        [TestMethod]
        public void RequestFloor_ShouldAcceptValidFloorNumber()
        {
            // Arrange
            var elevatorService = new ElevatorService(new Elevator(), 10);

            // Act
            elevatorService.RequestFloor(1);

            // Assert
            string output = Console.ReadLine();
            Assert.IsTrue(output.Contains($"Elevator {elevatorId}: Requesting floor 1"));
        }

        [TestMethod]
        public void ProcessRequests_ShouldMoveElevatorToNextRequestedFloor()
        {
         
            // Arrange
            var elevatorService = new ElevatorService(new Elevator(), 10);
            elevatorService.RequestFloor(3);
            elevatorService.currentFloor = 2;

            // Act
            elevatorService.ProcessRequests();

            // Assert
            Assert.AreEqual(elevatorService.currentFloor, 3);
        }

        [TestMethod]
        public void ProcessRequests_ShouldNotMoveElevatorIfOverweight()
        {
            // Arrange
            var elevatorService = new ElevatorService(new Elevator(), 10);
            elevatorService.currentFloor = 2;
            elevatorService.RequestFloor(3);
            elevatorService.currentWeight = elevatorService.maxWeightCapacity + 1;

            // Act
            elevatorService.ProcessRequests();

            // Assert
            Assert.AreEqual(elevatorService.currentFloor, 2);
        }

        [TestMethod]
        public void EnterPassengers_ShouldReturnCorrectWeight()
        {
            // Arrange
            var elevatorService = new ElevatorService(new Elevator(), 10);

            // Act
            var weight = elevatorService.EnterPassengers();

            // Assert
            Assert.AreEqual(weight, 80 * 2);
        }

        private static IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddScoped<IElevatorRepository, ElevatorRepository>()
                .AddScoped<IElevatorService, ElevatorService>()
                .AddScoped<Elevator>()
                .BuildServiceProvider();
        }
    }
}

