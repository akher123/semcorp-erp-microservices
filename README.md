# semcorp-erp-microservices
Technical Assessment: Senior Backend &amp; Infrastructure Engineer
## Part 1: The Core Backend Challenge (API & Database)

# Ordering Microservice

A robust **Order Ingestion Microservice** built with **.NET 10**, following **Clean Architecture** and **Domain-Driven Design (DDD)** principles. This service provides a RESTful API to accept orders from various e-commerce platforms, ensuring data integrity, high performance, and scalability.

## Features

- **RESTful API**: `POST /api/v1/orders` to create orders.
- **CQRS & MediatR**: Separates read/write operations and promotes clean command handling.
- **Clean Architecture & DDD**: Maintains modularity, testability, and scalability.
- **Entity Framework Core**: Efficient data access and persistence.
- **Validation**: Ensures order data integrity:
  - Quantity > 0
  - Price > 0
  - Valid email format
- **Idempotency**: Prevents duplicate orders based on a unique **OrderName**.
- **Asynchronous Processing**: Simulates integration with a third-party logistics gateway without blocking the API (2-second delay).
- **Swagger/OpenAPI**: Interactive API documentation for testing endpoints.
- **Common Cross-Cutting Concerns**:
  - Logging
  - Exception handling
  - Validation
  - Idempotency support
---

## ⚡ Features

1. **Domain Event Handling**  
   - Internal events that modify the domain state, e.g., `OrderCreatedEventHandler` updates order status.  

2. **Integration Event Handling**  
   - External events for communication with other systems, e.g., `LogisticsIntegrationEventHandler` calls `ILogisticsGateway`.  

3. **Async Logistics Gateway (Mock)**  
   - Simulates a 2-second delay for external logistics notifications.  
   - Generates mock tracking IDs.  

4. **Swagger UI**  
   - Test endpoints with JSON request bodies.  
   - Ensure `customerId` and `productId` exist in the database.

---
## Architecture
## Use Swagger to Test the API Endpoint with a JSON Body

You can test API endpoints directly in Swagger by sending a JSON request body. Follow these steps:

Open the Swagger UI for your API (usually at /swagger or /swagger/index.html).

Locate the endpoint you want to test.

Click “Try it out”.

Enter the JSON request body in the provided editor. For example:
Make sure all referenced IDs exist in the database, e.g., customerId and productId.
```csharp
{
  "Order": {
    "customerId": "B0CD2EC7-40F1-4BE7-8EC7-BDC193FC20C1",
    "emailAddress":"akher.ice07@gmail.com",
    "orderName": "ORD-001",

    "orderItems": [
      {
        "productId": "594CBD53-A046-4C3E-A321-7CAA62F340BC",
        "quantity": 1,
        "price":500
      },
      {
        "productId": "CC42E279-4335-4F0D-8A65-A450FE5EE40D",
        "quantity":1,
        "price":300
      }
    ]
  }
}
```
<img width="1613" height="991" alt="image" src="https://github.com/user-attachments/assets/e6518a28-60fe-4f16-b267-aa2c6a095e80" />

## Part 2: DevOps & Infrastructure Design:

<img width="1053" height="595" alt="image" src="https://github.com/user-attachments/assets/b3d675c5-ea49-41f0-9429-f62222df4b67" />


## Part3: Code Review & Mentorship Summary

```csharp
[HttpGet("get-order")]
public IActionResult GetOrder(string orderId)
{
    // Junior Dev Comment: Just getting the data quickly
    using (var conn = new SqlConnection("Server=myServer;Database=myDataBase;User Id=myUsername;Password=myPassword;"))
    {
        conn.Open();
        var cmd = new SqlCommand("SELECT * FROM Orders WHERE OrderId = '" + orderId + "'", conn);
        var reader = cmd.ExecuteReader();
        return Ok(reader);
    }
}
```
## SQL Injection Vulnerability

**Problem:** Query uses string concatenation, allowing SQL injection.

```csharp
var cmd = new SqlCommand("SELECT * FROM Orders WHERE OrderId = '" + orderId + "'", conn);
```

Example attack:

```
"123'; DROP TABLE Orders; --"
```

**Fix:** Always use parameterized queries and never trust user input.


## Hardcoded Credentials

**Problem:** Connection string contains embedded username/password.

```csharp
new SqlConnection("Server=myServer;Database=myDataBase;User Id=myUsername;Password=myPassword;");
```

**Risk:** Secrets can leak via Git or logs.

**Fix:** Use configuration files, environment variables, or a secret manager.


## Returning `SqlDataReader` Directly

**Problem:** Controller returns a `SqlDataReader` from the API.

* Cannot be serialized
* Exposes internal schema
* Breaks architecture layers

**Fix:**

* API → Service → Repository structure
* Convert results to objects, map to DTOs, return DTOs
