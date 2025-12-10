using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Events
{
    public record LogisticsGatewayIntegrationEvent(Order Order):INotification;

}
