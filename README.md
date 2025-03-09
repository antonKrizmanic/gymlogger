# GymLogger

  [![CI](https://github.com/antonKrizmanic/gymlogger/actions/workflows/buildAction.yaml/badge.svg)](https://github.com/antonKrizmanic/gymlogger/actions/workflows/buildAction.yaml)    

GymLogger is a web API for tracking workouts, primarily focused on gym exercises. The application is built using a Clean Architecture approach, ensuring separation of concerns, maintainability, and testability.

## 📖 Table of contents
- 🚀 [Technologies](#-technologies)
- 🏛️ [Architecture](#-architecture)
- 🔰 [Getting started](#-getting-started)
- 🛠️ [Prerequisites](#-prerequisites)
- 🤝 [Contributing](#-contributing)
- 🪪 [License](#-license)

## 🚀 Technologies
- [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0) - Cross-platform, high-performance, open-source framework for building modern, cloud-enabled, Internet-connected applications
- [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=visual-studio) - membership system that allows login functionality
- [Entity Framework Core 8](https://learn.microsoft.com/en-us/ef/core/) - lightweight, extensible, open source, and cross-platform object-relational mapper (O/RM)
- [AutoMapper](https://automapper.org/) - convention-based object-object mapper
- [PostgreSQL](https://www.bing.com/search?pglt=41&q=postgresql&cvid=3fbb17f0468943b08e8c4d44b664ec1a&gs_lcrp=EgZjaHJvbWUqBggBEC4YQDIGCAAQRRg7MgYIARAuGEAyBggCEEUYOTIGCAMQRRg7MgYIBBBFGDsyBggFEEUYPDIGCAYQRRg8MgYIBxBFGDwyBggIEEUYPNIBCDIyNjlqMGoxqAIAsAIA&FORM=ANNTA1&DAF0=1&PC=U531) - powerful, object-relational database system
- [Serilog](https://serilog.net/) - simple .NET logging with fully-structured events
- [xUnit](https://xunit.net/)
- [Moq](https://github.com/moq)
  
## 🏛️ Architecture

The solution is structured into several projects:

- **GymLogger.Server (GymLogger.Api)**: This is the server-side of the application, built using [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-5.0). It contains the controllers and endpoints for the API.

- **GymLogger.Application**: This layer contains the application's use cases, business logic, and services.

- **GymLogger.Domain (GymLogger.Core)**: This layer contains the domain entities and business rules.

- **GymLogger.Infrastructure.Database**: This layer contains code for interacting with the database.

- **GymLogger.Shared**: This project contains models and DTOs that are shared between the client and server.

## 🔰 Getting Started

To get started with the GymLogger application, you will need to have .NET 8 or later installed. You will also need a PostgreSQL database.

Once you have these prerequisites, you can clone the repository and start the application.

## 🛠️ Prerequisites
- .NET 9
- PostgreSQL
- IDE (Visual Studio or JetBrains Rider), or Text editor (Visual Studio Code)

## 🤝 Contributing

Contributions are welcome! Please feel free to submit an issue or create a pull request.

## 🪪 License

GymLogger is licensed under the MIT license. See the LICENSE file for more details.
