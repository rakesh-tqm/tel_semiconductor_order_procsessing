# Semiconductor Research Management Application

## Overview
This application manages semiconductor research data collected from various countries.
It aims to support technological innovation while promoting a sustainable, “Digital and Green” future.

## Technology Stack
- **Backend:** .NET 8.0, Entity Framework Core with column-level encryption
- **Database:** SQL Server, accessed via SqlClient and LINQ
- **Frontend:** HTML, jQuery, Bootstrap
- **Validation:** Backend validation using .NET validation; frontend validation using Bootstrap
- **Security:** All sensitive data in the database is encrypted using EF Core’s column encryption

## Architecture
The application follows a layered architecture separating concerns between data access, business logic, and presentation.
Entity Framework Core handles database interactions with encryption ensuring data security at rest.
The frontend is designed for responsiveness and user-friendly experience using Bootstrap and jQuery.

## Features
- Secure and encrypted storage of research and patient data
- Robust validation on both client and server sides to maintain data integrity
- Responsive UI design with Bootstrap for cross-device compatibility
- Efficient data querying and manipulation using LINQ and SqlClient
- Implement role-based access control (RBAC) for improved security
- Incorporate audit logging for tracking data changes and user actions

## Security Highlights
- Column-level encryption implemented via Entity Framework Core to protect sensitive data
- Backend validation to prevent invalid or malicious data inputs
- No sensitive data stored or transmitted in plain text

## Notes
- Ensure your SQL Server instance supports encryption features
- Maintain secure handling of encryption keys and connection strings
