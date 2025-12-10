using Microsoft.Extensions.Logging;
using Ordering.Application.Orders.Services;

namespace Ordering.Infrastructure.Integrations.Logistics;

public class MockLogisticsGateway(ILogger<MockLogisticsGateway> logger) : ILogisticsGateway
{
    public async Task NotifyOrderFulfillmentAsync(Order order, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Notifying logistics for Order {OrderName}...", order.OrderName);

        // Simulate 2-second async call to third-party system
        await Task.Delay(2000, cancellationToken);

        // Generate a mock tracking ID
        var trackingId = $"TRK-{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(1000, 9999)}";

        logger.LogInformation("Logistics system responded with Tracking ID {TrackingId} for Order {OrderName}",
            trackingId, order.OrderName);
    }


}
