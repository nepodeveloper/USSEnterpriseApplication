# USSEnterpriseApplication

USSEnterprise is a .NET console application that simulates the behaviour of multiple elevators running in parallel. It demonstrates how elevators respond to floor requests and showcases the concept of concurrent elevator operations.

# Getting Started
These instructions will help you set up and run the Elevator on your local machine.

## Prerequisites
•	.NET 6 SDK or higher
•	Visual Studio 2019 or higher

# Installing
1.	Clone the repository to your local machine:
bash Copy code
git clone [https://github.com/your-username/elevator-simulator.git](https://github.com/nepodeveloper/USSEnterpriseApplication.git) 
2.	Open the solution file USSEnterpriseApplication.sln in Visual Studio or your preferred code editor.
3.	Build the solution to restore dependencies:
Copy code
dotnet build 
Running the Simulator
1.	In your code editor, open the Program.cs file located in the ElevatorSimulator project.
2.	Inside the Main method, adjust the numberOfElevators variable to the desired number of elevators you want to simulate:
csharpCopy code
int numberOfElevators = 6; // Change this to the number of elevators you want to simulate 
3.	Run the application. Each elevator will run in a separate console window.
4.	Follow the on-screen prompts to interact with the elevator simulation. Enter a floor number to request a floor or enter 'q' to quit the simulation.
5.	
# Architecture
The project follows a Clean Architecture design with a focus on Domain-Driven Design principles. It is divided into the following layers:
•	USSEnterprise.Application: Contains application use cases and services.
•	USSEnterprise.Domain: Holds domain entities and value objects.
•	USSEnterprise.Infrastructure: Handles data access and external services.
•	USSEnterprise.Presentation: Contains the console application's entry point and user interface.
Contributing
Contributions are welcome! If you find a bug, have an enhancement, or want to propose a new feature, please create an issue or a pull request.

# License
This project is licensed under the MIT License - see the LICENSE file for details.


