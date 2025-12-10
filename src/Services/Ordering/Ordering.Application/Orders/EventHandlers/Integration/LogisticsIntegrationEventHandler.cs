using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Orders.EventHandlers.Domain;
using Ordering.Application.Orders.Services;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class LogisticsIntegrationEventHandler(ILogger<OrderCreaedEventHandler> logger, ILogisticsGateway logisticsGateway) : INotificationHandler<LogisticsGatewayIntegrationEvent>
{
    public async Task Handle(LogisticsGatewayIntegrationEvent notification, CancellationToken cancellationToken)
    {

        logger.LogInformation("Received LogisticsIntegrationEvent for Order {OrderNumber}", notification.Order.OrderName);

        await logisticsGateway.NotifyOrderFulfillmentAsync(notification.Order, cancellationToken);

        logger.LogInformation("Successfully notified logistics system for Order {OrderNumber}", notification.Order.OrderName);
    }
}
