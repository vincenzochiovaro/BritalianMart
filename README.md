# BritalianMart

## Overview

BritalianMart is a modular monolithic application developed using .NET and C#.
It offers a flexible and organized architecture.

## Tech Stack

- .NET
- C#
- xUnit
- CosmosDB 
- Azure Blob Storage
- Azure Queue Storage

## Project Structure

The project is organized into several modules:

- `BritalianMart`: The main application module.
- `BritalianMartTests`: Unit tests for the application.
- `BritalianMart.Catalog`: Module for catalog-related functionality.
- `BritalianMart.Reports`: Module responsible for generating and handling product reports.
  
### Prerequisites

- .NET SDK (version 7.0 or higher)
- Visual Studio


## CI/CD Pipeline

A Continuous Integration and Continuous Deployment (CI/CD) pipeline is in place to streamline development and deployment:

- **Environment Provisioning**: Environments (dev and prod) are created using Terraform on terraform.io.
- **Branch Deployment**: Code is automatically deployed upon merging into the main branch.
- **Testing**: Automated tests using xUnit, Fluent Assertion, Fixture, Moq, and Postman.