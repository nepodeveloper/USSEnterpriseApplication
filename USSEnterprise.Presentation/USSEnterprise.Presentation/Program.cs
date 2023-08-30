using Microsoft.Extensions.DependencyInjection;
using System;
using USSEnterprise.Application.Interfaces;
using USSEnterprise.Application.Services;
using USSEnterprise.Domain.Entities;
using USSEnterpriseApplication.Infrastructure.Data.Interfaces;
using USSEnterpriseApplication.Infrastructure.Data.Repositories;

namespace USSEnterprise.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            var elevatorService = serviceProvider.GetService<IElevatorService>();
            var elevator = new Elevator();

            while (true)
            {
                Console.Write("Please enter a floor number or 'q' to quit: ");
                string input = Console.ReadLine();

                if (input == "q")
                {
                    Console.Write("Goodbye! ");
                    break;
                }

                if (int.TryParse(input, out int floor))
                {
                    elevatorService.RequestFloor(floor);
                }
                else
                {
                    Console.Write("Invalid input. Please enter a valid floor number. ");
                }
            }
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
