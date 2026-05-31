# Clinic Management System

A modern Clinic Management System built with ASP.NET Core and Clean Architecture principles. The project provides secure authentication, appointment scheduling, patient management, medical records, payments, and prescription management.

## Features

* JWT Authentication
* Refresh Token & Logout
* Role-Based Authorization
* Ownership-Based Authorization Policies
* CQRS Pattern with MediatR
* Result Pattern
* FluentValidation
* Clean Architecture
* Entity Framework Core
* SQL Server Database

## Modules

### User Management

* Create, Update, Delete Users
* User Profiles
* Role Assignment

### Doctor Management

* Create, Update, Delete Doctors
* Search Doctors
* Doctor Information

### Patient Management

* Create, Update, Delete Patients
* Patient Information
* Medical History

### Appointment Management

* Book Appointments
* Update Appointments
* Cancel Appointments
* Complete Appointments
* Available Time Slots
* Pending Appointments

### Medical Records

* Create Medical Records
* Update Medical Records
* Retrieve Medical History

### Payments

* Payment Management
* Payment Tracking

### Prescriptions

* Create Prescriptions
* Update Prescriptions
* Prescription Tracking

## Architecture

The solution follows Clean Architecture principles and is organized into the following layers:

* **Clinic_Flow** – API Layer
* **Clinic_Application** – Application Layer
* **Clinic_Domain** – Domain Layer
* **Clinic_Infrastructure** – Infrastructure Layer

## Technologies

* ASP.NET Core
* C#
* Entity Framework Core
* SQL Server
* MediatR
* FluentValidation
* JWT Authentication
* Clean Architecture
* CQRS Pattern

## Security

* JWT Access Tokens
* Refresh Tokens
* Role-Based Authorization
* Ownership Policies
* Secure Password Hashing

## Future Improvements

* Unit Testing
* Audit Logging
* Email Notifications
* Docker Deployment
* Pagination
* Soft Delete
* API Versioning

## Author

Roqaia Sadaf

Backend Developer | ASP.NET Core | Clean Architecture | Flutter
