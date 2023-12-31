# BritalianMart

## Overview

BritalianMart is a modular monolithic application developed using .NET and C#, designed for efficient organization and flexibility. The primary focus is on providing a robust and scalable architecture.

## Tech Stack

- .NET
- C#
- xUnit
- CosmosDB 

## Project Structure

The project is organized into several modules:

- `BritalianMart`: The core application module.
- `BritalianMartTests`: Unit tests for the application.
- `BritalianMart.Catalog`: Module for catalog-related functionality.
- `BritalianMart.Reports`: This module streamlines the process of creating detailed reports that provide insights into the specified products within the application
  
### Prerequisites

- .NET SDK (version 7.0 or higher)
- Visual Studio
- 
### CI/CD Pipeline (Coming Soon)

A Continuous Integration and Continuous Deployment (CI/CD) pipeline has been implemented to streamline the development and deployment processes:

- Environment Provisioning: Environments (dev and prod) are provisioned using Terraform on terraform.io.
- Branch Deployment: Automatic deployment is triggered upon merging into the main branch.
- Testing: Comprehensive automated testing leveraging xUnit, Fluent Assertion, Fixture, Moq.
- Deployment Flow: Code progression through Dev ➡ Prod environments ensures reliability and stability.
