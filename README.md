# Invoicify Solution - In-Depth Programmer Documentation

## Solution Structure

- **Invoicify.Client**: Blazor WebAssembly frontend for user interaction.
- **Invoicify.Server**: ASP.NET Core Web API backend for business logic and data.
- **Data**: Shared domain models and DTOs.
- **FakeData**: Utilities for generating test data.
- **FileGenerator**: Utilities for exporting invoices to documents.
- **TestProject**: TBA Automated tests.

---

## Invoicify.Client (Blazor WebAssembly Frontend)

### Architecture
- Entry point: `Program.cs` configures the Blazor app and sets up an HTTP client for API communication.
- Uses Razor components for UI, with routing managed in the `Pages` folder.

### Main Features
#### Invoice Browser (`Pages/Invoice/InvoiceBrowser.razor`)
- Displays all invoices in an interactive table (`InteractiveTable.razor`).
- Allows selection of invoices to preview details, including parties, items, and payment info.
- Shows QR codes for payment using invoice data.
- Supports exporting invoices to HTML/PDF via server endpoints.

#### Invoice Creator (`Pages/Invoice/InvoiceCreator.razor`)
- Provides a form for creating and editing invoices.
- Integrates components for seller/customer info (`PartyInfoComponent.razor`), bank info (`BankInfoComponent.razor`), QR payment (`QrMiscComponent.razor`), orders (`OrderInfoComponent.razor`), and items (`InvoiceItemComponent.razor`).
- Includes buttons for saving, clearing, and filling with test data (using `FakeData`).
- Validates input using Blazor’s form validation.

#### QR Generator (`Pages/QrGenerator.razor`)
- Lets users generate and preview QR payment codes.
- Form fields for payment details, with instant QR preview.
- Clear/reset functionality for quick form management.

#### Help Page (`Pages/Help.razor`)
- Static documentation for users, describing main features and usage.

### Components
- **InteractiveTable.razor**: Generic table for displaying and selecting items.
- **InvoiceComponent.razor**: Handles invoice details.
- **PartyInfoComponent.razor**: Displays/edit party (seller/customer) info.
- **BankInfoComponent.razor**: Displays/edit bank info.
- **QrMiscComponent.razor**: Handles QR payment data.
- **InvoiceListComponent.razor**: Manages lists of orders/items.
- **InvoiceItemComponent.razor**: Displays/edit invoice items.
- **OrderInfoComponent.razor**: Displays/edit order info.

### Communication
- Uses `HttpClient` (configured in `Program.cs`) to interact with server API endpoints for CRUD operations and exporting.

---

## Invoicify.Server (ASP.NET Core Web API Backend)

### Architecture
- Entry point: `Program.cs` configures services, CORS, database context, and endpoint mappings.
- Uses Entity Framework Core with SQLite (`InvoicifyDbContext.cs`).
- Endpoints are modularly mapped in `Endpoints/EndpointMappingExtensions.cs`.

### Main Features
#### Invoice Endpoints
- **GET /invoice**: Returns all invoices with related entities (seller, customer, bank, items).
- **GET /invoice/{id:guid}**: Returns a single invoice by ID, including all details (addresses, orders, items).
- **POST endpoints**: For creating and updating invoices and related entities.
- **Export endpoints**: For generating HTML/PDF documents of invoices (uses `FileGenerator`).

#### CORS
- Configured for Blazor client (default: `http://localhost:5125`), allowing cross-origin requests.

#### OpenAPI/Swagger
- Enabled in development for interactive API documentation and testing.

### Database
- **InvoicifyDbContext.cs**: Manages tables for Invoice, PartyInfo, Address, BankInfo, OrderInfo, InvoiceItem.
- **Migrations/**: Contains EF Core migrations for schema management.

---

## Data (Shared Models)

### Domain Models
- **Invoice**: Main entity representing an invoice.
- **PartyInfo**: Seller/customer information.
- **Address**: Address details for parties.
- **BankInfo**: Bank details for payment.
- **OrderInfo**: Orders associated with invoices.
- **InvoiceItem**: Line items on invoices.
- **QRPayment**: Data for QR payment codes.
- **Bags/**: DTOs for transferring data between client and server.

### Usage
- Shared between Client and Server for consistency and type safety.

---

## FakeData

### Purpose
- Generates realistic test data for development and testing.
- Used in InvoiceCreator for quick form population.

---

## FileGenerator

### Purpose
- Generates and exports invoice documents (HTML/PDF).
- Used by server endpoints and client features for exporting invoices.

---

## TestProject

### Future purpose
- Will contain automated tests for solution components.
- Run with `dotnet test` to validate functionality.

---

## Development & Setup

1. Install .NET 8 SDK.
2. Restore NuGet packages: `dotnet restore`.
3. Build the solution: `dotnet build`.
4. Run database migrations if schema changes: `dotnet ef migrations add [name]` + `dotnet ef database update`.
5. Start the server: `dotnet run --project Invoicify.Server`.
6. Start the client: `dotnet run --project Invoicify.Client`.
7. Access Scalar UI at `localhost:[serverport]/scalar` (development only).

---

## Contributing

- Follow C# and Blazor best practices.
- Organize new endpoints in Server's `Endpoints/EndpointMappingExtensions.cs`.
- Add new domain models in `Data/DbModel`.
- Add new Blazor components in `Client/BlazorComponents`.
- Write tests in `TestProject`.
- Document new features in this README.
