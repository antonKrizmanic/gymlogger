# GymLogger

  [![CI](https://github.com/antonKrizmanic/gymlogger/actions/workflows/buildAction.yaml/badge.svg)](https://github.com/antonKrizmanic/gymlogger/actions/workflows/buildAction.yaml)  
  [![GitHub issues](https://img.shields.io/github/issues/antonKrizmanic/gymlogge?color=0088ff)](https://github.com/antonKrizmanic/gymlogge/issues)
  [![GitHub contributors](https://img.shields.io/github/contributors/antonKrizmanic/gymlogge)](https://github.com/antonKrizmanic/gymlogge/graphs/contributors)
  [![GitHub pull requests](https://img.shields.io/github/issues-pr/antonKrizmanic/gymlogge?color=0088ff)](https://github.com/antonKrizmanic/gymlogge/pulls)

GymLogger is a web application for tracking workouts, primarily focused on gym exercises. The application is built using a Clean Architecture approach, ensuring separation of concerns, maintainability, and testability.

## Architecture

The solution is structured into several projects:

- **GymLogger.Client**: This is the client-side of the application, built using [Blazor WebAssembly](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-5.0). It contains the user interface and components for the application.

- **GymLogger.Server (GymLogger.Api)**: This is the server-side of the application, built using [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-5.0). It contains the controllers and endpoints for the API.

- **GymLogger.Application**: This layer contains the application's use cases, business logic, and services.

- **GymLogger.Domain (GymLogger.Core)**: This layer contains the domain entities and business rules.

- **GymLogger.Infrastructure.Database**: This layer contains code for interacting with the database.

- **GymLogger.Shared**: This project contains models and DTOs that are shared between the client and server.

## Technologies

- **Blazor WebAssembly**: Used for building the client-side of the application. [Blazor WebAssembly](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-5.0) allows for building interactive web UIs using C# instead of JavaScript.

- **Fluent UI**: Used for building the user interface in the Blazor WebAssembly project. [Fluent UI](https://www.fluentui-blazor.net/) provides a set of accessible, reusable, and high-quality components for creating web applications.

- **ASP.NET Core Web API**: Used for building the server-side API of the application.

- **Entity Framework Core**: Used as the Object-Relational Mapper (ORM) for data access. You can learn more about [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) here.

- **SQL Server**: Used as the database for the application.

## Getting Started

To get started with the GymLogger application, you will need to have .NET 8 or later installed. You will also need a SQL Server database.

Once you have these prerequisites, you can clone the repository and start the application.

## Contributing

Contributions are welcome! Please feel free to submit a pull request.

## License

GymLogger is licensed under the MIT license. See the LICENSE file for more details.
