# semcorp-erp-microservices
Technical Assessment: Senior Backend &amp; Infrastructure Engineer

Part 2: DevOps & Infrastructure Design:
<img width="1053" height="595" alt="image" src="https://github.com/user-attachments/assets/b3d675c5-ea49-41f0-9429-f62222df4b67" />


Security Issue — SQL Injection Vulnerability
❌ Problem

The query is built using string concatenation:

var cmd = new SqlCommand("SELECT * FROM Orders WHERE OrderId = '" + orderId + "'", conn);


This allows a malicious user to inject SQL.
Example attack:

orderId = "123'; DROP TABLE Orders; --"

✅ Explanation for the Junior Developer

Always use parameterized queries (or stored procedures).

Never trust client inputs — validate and sanitize everything.

🔑 Security Issue — Hardcoded Credentials in Code
❌ Problem

The connection string is directly embedded in the code:

new SqlConnection("Server=myServer;Database=myDataBase;User Id=myUsername;Password=myPassword;");

⚠️ Risks

Credentials can leak through Git history, logs, or shared code.

Violates OWASP recommendations for secure secret handling.

✅ Explanation for the Junior Developer

Use:

Configuration files

Environment variables

A secure secret manager (Azure Key Vault, AWS Secrets Manager, etc.)

🚧 Performance / API Design Issue — Returning SqlDataReader Directly
❌ Problem

The controller directly queries the database and returns a SqlDataReader in the API response.

⚠️ Issues

The API layer should not access the database directly.

SqlDataReader cannot be serialized to JSON.

It becomes invalid once the database connection closes.

Leaks internal database schema.

Risks over-posting or exposing sensitive fields.

✅ Explanation for the Junior Developer

The API should only handle HTTP requests/responses.

Use proper architecture:

Controller → Service Layer → Data Access Layer (Repository)

Materialize the result into objects.

Map entities into DTOs.

Return DTOs from the API to ensure:

Safe serialization

No schema leakage

Only intended fields are exposed
