# Invoicify
Invoicify is a simple application for managing invoices and related data.
It allows users to create, view, and manage invoices, involved parties, orders and more using a local SQLite database.
Invoicify itself is designed as a Server-Client application with a REST API on the server side and Blazor Web application on the client side.

## Features
- Create, store and manage invoices
- Generate HTML and PDF invoices with formatting
- Generate QR codes for fast and typo-proof payment
- Store, manage, and get auto-complete suggestions for invoice-related data
    - Involved parties
    - Orders
    - Invoice items
- Server-Client architecture for ease of use
    - Self-host server-side yourself and access it from any device
    - Use included Blazor client application...
      - ...or not! Implement your own client using the REST API...
      - ...or send raw JSON over HTTP (but we are not savages, so stick to previous options)

### Future Features
- Create, store and manage orders
- Generate HTML and PDF orders with formatting

### What is an invoice?
An invoice is a document containing information about two parties (Customer and Provider/Seller) and items, services or other billables, that one party is requesting payment for from the party other.

Typical invoice has to contain:
- Information about both parties
    - Company name or First name and Last name
    - VAT number
    - Residence
    - Bank information (at least of the Seller) or other way the invoice ought to be paid
- List of billables that Customer is paying for to the Provider
- Information about every item
    - Name
    - Optionally description and root order
    - Unit
    - Price per unit
    - Total cost for bought number of units
- Subtotal without taxes
- Total price including applicable taxes

Additionaly, an invoice might contain:
- Logo of the Seller or both parties
- Information about Orders the Invoice is a response for
- QR Code for an easy payment

## Used packages and libraries
- [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor/) for the client-side web application
- [Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api/) (AspNetCore) for the server-side REST API
- [Entity Framework](https://learn.microsoft.com/en-us/ef/core/) Core for database access with SQLite
- [Playwright](https://playwright.dev/dotnet/docs/api/class-playwright/) for HTML and PDF generation
- [Crc32.NET](https://github.com/force-net/Crc32.NET/) and [QRCoder](https://github.com/codebude/QRCoder/) for QR code generation