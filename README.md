# TicketSupportSystem

**Description**
This is an ASP.NET Core project to create a Ticket Support System. The application allows customers to create support tickets and track their status. Support agents can be assigned to these tickets and are responsible for resolving them.

**Features**
- User Registration and Login
- Role-based Authorization (Customer, Support Agent, Admin)
- Create, Read, Update, and Delete Tickets
- Create, Read, Update, and Delete Comments on Tickets
- Ticket Sorting, Filtering and Pagination
- Downloading, removing and getting attachments to comments

**Technologies**
- ASP.NET Core 6.0
- Entity Framework Core 6.0
- SQL Server
- JWT for Authentication

**Setup**
To run this project, install it locally using dotnet:
```
dotnet restore 
dotnet ef database update 
dotnet run
```
