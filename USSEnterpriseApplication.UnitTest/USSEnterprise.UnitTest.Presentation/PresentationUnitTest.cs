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
using USSEnterprise.Presentation;
using USSEnterpriseApplication.Infrastructure.Data.Interfaces;
using USSEnterpriseApplication.Infrastructure.Data.Repositories;

namespace USSEnterpriseApplication.UnitTest.USSEnterprise.UnitTest.Presentation
{
    [TestClass]
    class PresentationUnitTest
    {
       
        [TestMethod]
        public async Task SimulateElevator_ShouldWaitFor3000Milliseconds()
        {
            // Arrange
            var serviceProvider = ConfigureServices();
            var elevatorId = 1;

            // Act
            var task = Program.SimulateElevator(elevatorId, serviceProvider);

            // Assert
            await task;
            Assert.IsTrue(task.IsCompleted);           
        }

        [TestMethod]
        public async Task SimulateElevator_ShouldPrintElevatorIsReadyMessage()
        {
            // Arrange
            var serviceProvider = ConfigureServices();
            var elevatorId = 1;

            // Act
            var task = Program.SimulateElevator(elevatorId, serviceProvider);

            // Assert
            await task;
            string output = Console.ReadLine();
            Assert.IsTrue(output.Contains($"Elevator {elevatorId} is ready."));
        }

        [TestMethod]
        public async Task SimulateElevator_ShouldPromptForFloorNumber()
        {
            // Arrange
            var serviceProvider = ConfigureServices();
            var elevatorId = 1;

            // Act
            var task = Program.SimulateElevator(elevatorId, serviceProvider);

            // Assert
            await task;
            string output = Console.ReadLine();
            Assert.IsTrue(output.Contains($"Elevator {elevatorId}: Please enter a floor number or 'q' to quit:"));
        }

        [TestMethod]
        public async Task SimulateElevator_ShouldRequestFloorWhenValidFloorNumberIsEntered()
        {
            // Arrange
            var serviceProvider = ConfigureServices();
            var elevatorId = 1;

            // Act
            var task = Program.SimulateElevator(elevatorId, serviceProvider);

            // Assert
            await task;
            string output = Console.ReadLine();
            Assert.IsTrue(output.Contains($"Elevator {elevatorId}: Requesting floor 1"));
        }

        [TestMethod]
        public async Task SimulateElevator_ShouldPrintGoodbyeMessageWhenQuitIsEntered()
        {
            // Arrange
            var serviceProvider = ConfigureServices();
            var elevatorId = 1;

            // Act
           
            var task =  Program.SimulateElevator(elevatorId, serviceProvider);

            // Assert
            await task;
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
