# BritalianMart

## Overview

BritalianMart is a modular monolithic application developed using .NET and C#.
It offers a flexible and organized architecture.

## Tech Stack

- .NET
- C#
- xUnit
- Fluent Assertion
- Fluent Validation
- Postman
- Fixture 
- Moq 

## Project Structure

The project is organized into several modules:

- `BritalianMart`: The main application module.
- `BritalianMartTests`: Unit tests for the application.
- `BritalianMart.Catalog`: Module for catalog-related functionality.
- `BritalianMart.StockManagement`: Coming Soon
  
### Prerequisites

- .NET SDK (version 7.0 or higher)
- Visual Studio


## CI/CD Pipeline

A Continuous Integration and Continuous Deployment (CI/CD) pipeline is in place to streamline development and deployment:

- **Environment Provisioning**: Environments (dev, test, prod) are created using Terraform on terraform.io.
- **Branch Deployment**: Code is automatically deployed upon merging into the main branch.
- **Testing**: Automated tests using xUnit, Fluent Assertion, Fixture, Moq, and Postman.
- **Deployment Flow**: Dev ➡ Test ➡ Prod ensures code reliability and stability.

### API Documentation (Coming Soon)
### Messaging System (Coming Soon)
