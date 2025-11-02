Ecommerce Microservices Challenge
Project Overview

This project is a microservices-based application designed to manage product inventory and sales for an e-commerce platform. It demonstrates clean architecture, modularity, and scalable design principles suitable for real-world applications.

The system consists of two main microservices:

Product Inventory Service – Manages product stock, availability, and updates.

Sales Service – Handles orders, order items, and sales transactions.

These services communicate through an API Gateway, enabling decoupled and maintainable microservice interactions.

Key Features

Microservices Architecture – Independent, modular services to improve scalability and maintainability.

Event-Driven Communication – Uses RabbitMQ for asynchronous messaging between services.

Secure Authentication – JWT-based authentication for API security.

Data Persistence – Entity Framework and relational database integration for reliable data storage.

RESTful APIs – Provides endpoints for service consumption and integration with frontend systems.

Technologies

Backend: .NET Core, C#

Database: Relational Database (e.g., PostgreSQL or SQL Server)

ORM: Entity Framework Core

Messaging: RabbitMQ

Security: JWT Authentication

API Communication: RESTful APIs through API Gateway

Getting Started

Clone the repository:

git clone https://github.com/Davi-cyber2511/EcommerceService.git


Open the solution in Visual Studio or your preferred IDE.

Configure the database connection and RabbitMQ settings in each service.

Run the microservices independently or deploy using Docker for containerization.

Notes

Database migration scripts are not included. The entity classes define the required schema.

Microservices are ready for integration with frontend applications via REST APIs.

The architecture allows easy addition of new services or features in the future.

Future Enhancements

Add comprehensive logging and monitoring for each service.

Implement Swagger/OpenAPI documentation for all endpoints.

Integrate automated testing for unit, integration, and end-to-end scenarios.

