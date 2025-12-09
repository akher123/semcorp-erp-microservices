# semcorp-erp-microservices
Technical Assessment: Senior Backend &amp; Infrastructure Engineer

Part 2: DevOps & Infrastructure Design:
<img width="1053" height="595" alt="image" src="https://github.com/user-attachments/assets/b3d675c5-ea49-41f0-9429-f62222df4b67" />


## Part3: Code Review & Mentorship Summary

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
