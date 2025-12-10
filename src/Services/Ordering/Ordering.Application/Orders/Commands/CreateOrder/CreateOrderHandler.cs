using Ordering.Application.Exceptions;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
   {
       // Check if an order with the same OrderName already exists for this customer

       var orderDto = command.Order;

        bool exists = await dbContext.Orders
                         .AnyAsync(o => o.CustomerId ==CustomerId.Of(orderDto.CustomerId)
                         && o.OrderName ==OrderName.Of(orderDto.OrderName),cancellationToken);
        if (exists)
        {
            throw new OrderAlreadyExistsException(
                message: "An order with the same ordername already exists for this customer.",
                details: $"CustomerId: {orderDto.CustomerId}, OrderName: {orderDto.OrderName}"
            );
        }

        // Create new order
        var order = CreateNewOrder(command.Order);
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDto orderDto)
    {

        var newOrder = Order.Create(id: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(orderDto.CustomerId),
            orderName: OrderName.Of(orderDto.OrderName),
            emailAddress: EmailAddress.Of(orderDto.EmailAddress
            ));

        foreach (var orderItem in orderDto.OrderItems)
        {
            newOrder.Add(
                productId: ProductId.Of(orderItem.ProductId),
                quantity: orderItem.Quantity,
                price: orderItem.Price);
        }

        return newOrder;
    }
}
