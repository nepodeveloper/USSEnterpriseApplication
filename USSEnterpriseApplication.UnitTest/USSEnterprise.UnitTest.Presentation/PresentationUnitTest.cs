using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using USSEnterprise.Application.Interfaces;
using USSEnterprise.Application.Services;
using USSEnterprise.Domain.Entities;
using USSEnterprise.Presentation;
using USSEnterpriseApplication.Infrastructure.Data.Interfaces;
using USSEnterpriseApplication.Infrastructure.Data.Repositories;

namespace USSEnterpriseApplication.UnitTest.USSEnterprise.UnitTest.Presentation
{
    [TestClass]
    public class PresentationUnitTest
    {
        IServiceProvider serviceProvider = ConfigureServices();
        int elevatorId = 1;

        [TestMethod]
        public void SimulateElevator_ShouldWaitFor3000Milliseconds()
        {

            // Act
            var task = Program.SimulateElevator(elevatorId, serviceProvider);

            // Assert
            Assert.IsTrue(task.IsCompleted);
        }

        [TestMethod]
        public void SimulateElevator_ShouldPrintElevatorIsReadyMessage()
        {
            // Act
            var task = Program.SimulateElevator(elevatorId, serviceProvider);

            // Assert
            string output = Console.ReadLine();
            Assert.IsTrue(output.Contains($"Elevator {elevatorId} is ready."));
        }

        [TestMethod]
        public void SimulateElevator_ShouldPromptForFloorNumber()
        {
            // Act
            var task = Program.SimulateElevator(elevatorId, serviceProvider);

            // Assert
            string output = Console.ReadLine();
            Assert.IsTrue(output.Contains($"Elevator {elevatorId}: Please enter a floor number or 'q' to quit:"));
        }

        [TestMethod]
        public void SimulateElevator_ShouldRequestFloorWhenValidFloorNumberIsEntered()
        {
            // Act
            var task = Program.SimulateElevator(elevatorId, serviceProvider);

            // Assert
            string output = Console.ReadLine();
            Assert.IsTrue(output.Contains($"Elevator {elevatorId}: Requesting floor 1"));
        }

        [TestMethod]
        public void SimulateElevator_ShouldPrintGoodbyeMessageWhenQuitIsEntered()
        {
            // Act
            var task = Program.SimulateElevator(elevatorId, serviceProvider);

            // Assert
            string output = Console.ReadLine();
            Assert.IsTrue(output.Contains($"Elevator {elevatorId}: Goodbye!"));
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
