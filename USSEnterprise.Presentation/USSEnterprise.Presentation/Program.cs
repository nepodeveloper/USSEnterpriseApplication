using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using USSEnterprise.Application.Interfaces;
using USSEnterprise.Application.Services;
using USSEnterprise.Domain.Entities;
using USSEnterpriseApplication.Infrastructure.Data.Interfaces;
using USSEnterpriseApplication.Infrastructure.Data.Repositories;

namespace USSEnterprise.Presentation
{
    class Program
    {
        private const string QUIT = "q";
        static async Task Main(string[] args)
        {
            List<Task> elevatorTasks = new List<Task>();

            Console.Write("How many elevator does the USS Enterprise have? ");
            int turbolifts = int.Parse(Console.ReadLine());

            for (int i = 1; i <= turbolifts; i++)
            {
                elevatorTasks.Add(SimulateElevator(i));
            }

            await Task.WhenAll(elevatorTasks);

            Console.WriteLine("All elevators have completed their tasks. Exiting...");


        }

        static Task SimulateElevator(int elevatorId)
        {
            var serviceProvider = ConfigureServices();

            var elevatorService = serviceProvider.GetService<IElevatorService>();      

            Console.WriteLine($"Elevator {elevatorId} is ready.");

            while (true)
            {
                Console.Write($"Elevator {elevatorId}: Please enter a floor number or 'q' to quit:");
                string input = Console.ReadLine();

                if (input == QUIT)
                {
                    Console.Write($"Elevator {elevatorId}: Goodbye!");
                    break;

                }

                if (int.TryParse(input, out int floor))
                {
                    elevatorService.RequestFloor(floor);
                }
                else
                {
                    Console.Write($"Elevator {elevatorId}: Invalid input. Please enter a valid floor number.");
                }
            }

            return Task.CompletedTask;
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
