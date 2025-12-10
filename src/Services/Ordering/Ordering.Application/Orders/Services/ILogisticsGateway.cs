using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Orders.Services;

public interface ILogisticsGateway
{
    Task NotifyOrderFulfillmentAsync(Order order, CancellationToken cancellationToken);
}