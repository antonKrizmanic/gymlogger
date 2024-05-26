# GymLogger

  [![CI](https://github.com/antonKrizmanic/gymlogger/actions/workflows/buildAction.yaml/badge.svg)](https://github.com/antonKrizmanic/gymlogger/actions/workflows/buildAction.yaml)    

GymLogger is a web application for tracking workouts, primarily focused on gym exercises. The application is built using a Clean Architecture approach, ensuring separation of concerns, maintainability, and testability.

## 📖 Table of contents
+ 🚀 [Technologies](#-technologies)
+ 🏛️ [Architecture](#-architecture)
+ 🔰 [Getting started](#-getting-started)
+ 🛠️ [Prerequisites](#-prerequisites)
+ 🤝 [Contributing](#-contributing)
+ 🪪 [License](#-license)

## 🚀 Technologies
+ [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0) - Cross-platform, high-performance, open-source framework for building modern, cloud-enabled, Internet-connected applications
+ [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=visual-studio) - membership system that allows login functionality
+ [Entity Framework Core 7](https://learn.microsoft.com/en-us/ef/core/) - lightweight, extensible, open source, and cross-platform object-relational mapper (O/RM)
+ [AutoMapper](https://automapper.org/) - convention-based object-object mapper
+ [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) - powerful, object-relational database system
+ [Serilog](https://serilog.net/) - simple .NET logging with fully-structured events
+ [xUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/) and [Moq](https://github.com/moq)
+ [Blazor WebAssembly](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-5.0) - allows for building interactive web UIs using C# instead of JavaScript.
+ [Fluent UI](https://www.fluentui-blazor.net/) provides a set of accessible, reusable, and high-quality components for creating web applications.
  
## 🏛️ Architecture

The solution is structured into several projects:

- **GymLogger.Client**: This is the client-side of the application, built using [Blazor WebAssembly](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-5.0). It contains the user interface and components for the application.

- **GymLogger.Server (GymLogger.Api)**: This is the server-side of the application, built using [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-5.0). It contains the controllers and endpoints for the API.

- **GymLogger.Application**: This layer contains the application's use cases, business logic, and services.

- **GymLogger.Domain (GymLogger.Core)**: This layer contains the domain entities and business rules.

- **GymLogger.Infrastructure.Database**: This layer contains code for interacting with the database.

- **GymLogger.Shared**: This project contains models and DTOs that are shared between the client and server.

## 🔰 Getting Started

To get started with the GymLogger application, you will need to have .NET 8 or later installed. You will also need a SQL Server database.

Once you have these prerequisites, you can clone the repository and start the application.

## 🛠️ Prerequisites
- .NET 8
- MS SQL Server
- IDE (Visual Studio or JetBrains Rider), or Text editor (Visual Studio Code)

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a pull request.

## 🪪 License

GymLogger is licensed under the MIT license. See the LICENSE file for more details.
