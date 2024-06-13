# MSAuth

👨‍💻 **UNDER DEVELOPMENT**

To understand key concepts of CLEAN architecture, Domain-Driven Design (DDD), Dependency Injection, Outbox patterns using Hangfire, and Microservices, I've developed MSAuth (Authentication Microservice). The goal is to create a service that can be easily consumed by other services.

This microservice enables the creation of apps, each with its unique app key, which allows them to manage user creation and authentication.

## To-Do List

- [X] Base layers (CLEAN architecture, DDD)
- [x] Dependency injection
- [X] Unit Of Work
- [X] Notification Context
- [X] Notification Filter
- [X] Model validation errors
- [X] Model Error Context
- [X] Model Error Filter
- [X] Add FluentValidation to simplify model validation
- [X] AutoMapper
- [ ] All DTO mappings
- [X] Email service (mocked) + hangfire
- [X] Install and simple example of Hangfire
- [ ] Implement all endpoints
- [X] Use ASP.NET Identity
- [X] JSON Web Tokens (JWT) generation
- [ ] Refresh tokens
- [X] Docker containerization
- [ ] Create an API Gateway to use multiple MS
- [ ] Create a front-end to consume the back-end services

## Run the project

👨‍💻 **UNDER DEVELOPMENT**

Open the project with Visual Studio and run it using Docker :)  
If you can't connect to your SQL Server database, you might need to do this: https://vivekcek.wordpress.com/2018/06/10/connecting-to-local-or-remote-sql-server-from-docker-container/