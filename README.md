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
- [ ] Migrate creating/updating/returning/deleting objects to Domain Services, and leave inter-entity business rules to AppServices
- [ ] Email service + hangfire
- [X] Install and simple example of Hangfire
- [ ] Implement all endpoints
- [ ] JWT Authentication
- [ ] Docker containerization
- [ ] Create an API Gateway to use multiple MS
- [ ] Create a front-end to consume the back-end services
