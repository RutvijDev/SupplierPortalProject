# Supplier Portal

A full-stack logistics and order fulfillment web application built with ASP.NET Core MVC and Microsoft SQL Server. This project simulates a real-world supply chain by managing products, orders, and deliveries across a set of distinct user roles.

## Key Features

- **Role-Based Access Control:** Secure authentication and authorization tailored for five distinct user roles (Admin, Supplier, Warehouse Manager, Delivery Person, Customer).
- **Full CRUD Functionality:** Suppliers have complete Create, Read, Update, and Delete control over their product listings.
- **Multi-Step Order Tracking:** The application follows an order's journey from placement to final delivery, with status updates at each stage.
- **Inventory Management:** Products have a defined stock quantity, preventing customers from ordering items that are unavailable.
- **Session Management:** Utilizes cookie-based authentication to maintain user sessions.

## Technology Stack

- **Backend:** ASP.NET Core MVC (.NET)
- **Database:** Microsoft SQL Server
- **Frontend:** Razor Pages, HTML, CSS, JavaScript (likely with Bootstrap)
- **Authentication:** ASP.NET Core Identity / Custom Cookie Authentication

## Roles & Responsibilities

The application is built around a clear separation of duties:

- **Admin:**
  - Manages user accounts for Suppliers, Warehouse Managers, and Delivery Persons (Add/Delete).
  - Manages warehouse information (Add/Delete).
  - Can view and update their own profile.
- **Supplier:**
  - Manages their product inventory (Add, Update, View, Delete).
  - Views new orders placed by customers.
  - Updates order status to "Shipped to Warehouse".
  - Can view their own profile.
- **Warehouse Manager:**
  - Views orders that have been shipped by suppliers.
  - Updates order status to "Checked-in at Warehouse" upon receipt.
  - Can view their own profile.
- **Delivery Person:**
  - Views a pool of available orders that are checked-in at the warehouse.
  - Accepts an order for delivery by entering its Order ID.
  - Updates the order status to "Delivered" upon completion.
  - Can view their own profile.
- **Customer (For Testing):**
  - This role is implemented primarily to test the end-to-end workflow.
  - Can view products, place an order, and track their order status.

## Core Order Workflow

1.  A **Supplier** adds a product with a specific inventory count.
2.  A **Customer** places an order for an in-stock product.
3.  The **Supplier** views the new order and updates its status to "Shipped to Warehouse".
4.  The **Warehouse Manager** receives the item and updates the status to "Checked-in at Warehouse".
5.  A **Delivery Person** accepts the order from a pool of available deliveries.
6.  The **Delivery Person** completes the delivery and updates the status to "Delivered".

## Project Scope & Limitations

- **Manual System:** The application does not include automated email or real-time notifications. Users must log in to check for updates.
- **Admin Role:** The Admin's role is strictly limited to user and warehouse management, with no oversight on active orders.
- **Linear Workflow:** The system implements the primary "happy path" for an order and does not include functionality for cancellations or returns.
