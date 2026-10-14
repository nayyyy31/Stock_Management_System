Stock Management System (SMS)
A full-stack cross-platform inventory and sales tracking application developed using .NET MAUI Blazor Hybrid, ASP.NET Core Web API, and Entity Framework Core with PostgreSQL.
Overview
The Stock Management System is designed to manage day-to-day warehouse and retail inventory operations. It supports master stock configuration, real-time inventory adjustments via purchases and sales transactions, full invoice modification/deletion workflows, and reporting.
Key Features
Master Stock Setup: Manage inventory items, item codes, descriptions, and base prices.   
Purchase Invoicing: Record vendor supplies with auto-incrementing invoice sequences, line-item entry, and unit pricing.   
Sales Processing: Process customer sales, compute order totals, and update stock counts dynamically.   
Invoice Modification & Deletion: Dedicated edit workflows (/purchase-edit and /sales-edit) allowing updates to existing invoice details, line items, and invoice removal.   Business Reporting: 
Filter transactions by date range for purchases and sales with automated totals computation and thermal print preview support.  

Technology Stack
Framework: .NET (C#)UI Layer: .NET MAUI Blazor Hybrid (HTML5 / Scoped CSS / Razor Components)   
ORM: Entity Framework CoreDatabase: PostgreSQL (via Npgsql.EntityFrameworkCore.PostgreSQL)
Architecture Pattern: Clean architecture with IDbContextFactory for UI thread isolation


Author
Author: Nay Chi Thinn

Role: Junior Software Developer

GitHub: @nayyyy31

LinkedIn: in/nay-chi-thinn