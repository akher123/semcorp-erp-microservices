using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreaedEventHandler(IMediator publisher, IFeatureManager featureManager, ILogger<OrderCreaedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {

        if(await featureManager.IsEnabledAsync("EnableLogisticsGateway"))
        {
           await  publisher.Publish(new LogisticsGatewayIntegrationEvent(notification.Order), cancellationToken);
        }

    }
}