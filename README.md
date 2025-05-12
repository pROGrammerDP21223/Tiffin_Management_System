# 🥗 Tiffin Management System API

A RESTful API built with **ASP.NET Core** for managing users, tiffin orders, payments, deliveries, and admin analytics in a subscription-based meal delivery service.

---

## 📌 Features

- **User Management**: Register, update, delete, and retrieve user profiles.
- **Order Management**: Create, update, view, and cancel tiffin orders.
- **Payment Processing**: Track payments for specific orders with status updates.
- **Delivery Tracking**: Maintain and update delivery statuses for daily meals.
- **Analytics Reporting**: Admin-side insights based on weekly, monthly, yearly, or complete data.

---

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core Web API
- **Data Access**: Entity Framework Core
- **Database**: SQL Server
- **Tools**: Swagger UI, LINQ, Dependency Injection, DTOs
- **Logging**: ILogger

---

## 🚀 Getting Started

### Prerequisites

- [.NET 6 SDK or later](https://dotnet.microsoft.com/)
- SQL Server or SQLite installed
- Visual Studio or VS Code

### Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/your-username/tiffin-management-api.git
   cd tiffin-management-api
   ```

2. **Configure the Database**

   Update `appsettings.json` with your database connection string.

3. **Apply Migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the Application**
   ```bash
   dotnet run
   ```

5. **Test Endpoints**
   Open your browser and visit:
   ```
   http://localhost:{port}/swagger
   ```

---

## 📮 API Endpoints

### 👤 User Controller (`/api/user`)

| Method | Route           | Description               |
|--------|------------------|---------------------------|
| GET    | `/`              | Get all users             |
| GET    | `/{id}`          | Get user by ID            |
| POST   | `/`              | Create a new user         |
| PUT    | `/{id}`          | Update an existing user   |
| DELETE | `/{id}`          | Delete a user             |

### 📦 Order Controller (`/api/order`)

| Method | Route                 | Description                     |
|--------|------------------------|---------------------------------|
| GET    | `/`                    | Get all orders                  |
| GET    | `/{id}`                | Get order details by ID         |
| GET    | `/user/{userId}`       | Get orders by user ID           |
| POST   | `/`                    | Create a new order              |
| PUT    | `/{id}`                | Update order status             |
| DELETE | `/{id}`                | Cancel an order                 |

### 💳 Payment Controller (`/api/payment`)

| Method | Route               | Description                      |
|--------|----------------------|----------------------------------|
| GET    | `/order/{orderId}`   | Get payment by order ID          |
| POST   | `/`                  | Create a new payment             |
| PUT    | `/{id}`              | Update payment status            |

### 🚚 Delivery Controller (`/api/delivery`)

| Method | Route               | Description                       |
|--------|----------------------|-----------------------------------|
| GET    | `/order/{orderId}`   | Get delivery by order ID          |
| POST   | `/`                  | Create a new delivery             |
| PUT    | `/{id}`              | Update delivery status            |

### 📊 Analytics Controller (`/api/analytics`)

| Method | Route                   | Description                                       |
|--------|--------------------------|---------------------------------------------------|
| GET    | `?timeframe=weekly`      | Get weekly analytics                             |
| GET    | `?timeframe=monthly`     | Get monthly analytics (default)                  |
| GET    | `?timeframe=yearly`      | Get yearly analytics                             |
| GET    | `?timeframe=all`         | Get analytics for all time                       |

---

## 🧩 Project Structure

```
TiffinManagement/
│
├── Controllers/
│   ├── UserController.cs
│   ├── OrderController.cs
│   ├── PaymentController.cs
│   ├── DeliveryController.cs
│   └── AnalyticsController.cs
│
├── Models/
│   ├── User.cs
│   ├── Order.cs
│   ├── Payment.cs
│   ├── Delivery.cs
│   └── TiffinPlan.cs
│
├── Services/
│   ├── Interfaces and Implementations
│
├── Repository/
│   └── AppDbContext.cs
│
├── appsettings.json
└── Program.cs
```

---

## ✅ Example Request (cURL)

```bash
curl -X POST http://localhost:5000/api/user -H "Content-Type: application/json" -d '{"fullName":"John Doe","email":"john@example.com"}'
```

---

## 📊 Sample Analytics Output

```json
{
  "totalOrders": 14,
  "ordersByStatus": {
    "Active": 6,
    "Cancelled": 1,
    "Completed": 3,
    "Pending": 4
  },
  "popularTiffinPlans": {
    "Deluxe Veg": 5,
    "Basic Veg": 1,
    "Basic Non-Veg": 1,
    "Deluxe Non-Veg": 1,
    "Keto Plan": 1
  },
  "totalRevenue": 23110,
  "deliverySuccessRate": 70,
  "newOrdersThisWeek": 0,
  "newCustomers": 9,
  "returningCustomers": 0
}
```

---

## 🤝 Contributing

1. Fork the repository
2. Create a new branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add your feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Create a Pull Request

---

## 📄 License

This project is licensed under the **MIT License**. See `LICENSE` for details.

---

## 📬 Contact

For any queries or suggestions, feel free to reach out at: **dhananjayphirke@gmail.com**
