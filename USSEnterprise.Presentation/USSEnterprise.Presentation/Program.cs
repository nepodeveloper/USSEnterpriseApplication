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
            // Create and configure the service provider
            IServiceProvider serviceProvider = ConfigureServices();

            Console.Write("How many elevator does the USS Enterprise have? ");
            int numberOfElevators = int.Parse(Console.ReadLine());

            List<Task> elevatorsAvailable = new List<Task>();

            for (int i = 1; i <= numberOfElevators; i++)
            {
                elevatorsAvailable.Add(SimulateElevator(i, serviceProvider));
            }

            await Task.WhenAll(elevatorsAvailable);

            Console.WriteLine("All elevators have completed their tasks. Exiting...");

        }

        static async Task SimulateElevator(int elevatorId, IServiceProvider serviceProvider)
        {
            await Task.Delay(3000);

            await Task.Run(() =>
            {           
                IElevatorService elevator = serviceProvider.GetService<IElevatorService>();

                Console.Write($"Elevator {elevatorId} is ready.");

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
                        elevator.RequestFloor(floor);
                    }
                    else
                    {
                        Console.Write($"Elevator {elevatorId}: Invalid input. Please enter a valid floor number.");
                    }
                }
            });  
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
