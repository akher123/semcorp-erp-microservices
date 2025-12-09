namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order):ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid OrderId);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        // Validate the Order object itself
        RuleFor(x => x.Order)
            .NotNull()
            .WithMessage("Order is required.")
            .WithErrorCode("ORDER_REQUIRED");

        // CustomerId validation
        RuleFor(x => x.Order.CustomerId)
            .NotEmpty()
            .WithMessage("CustomerId is required.")
            .WithErrorCode("CUSTOMER_ID_REQUIRED");

        // Email validation if present
        RuleFor(x => x.Order.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .WithErrorCode("EMAIL_REQUIRED")
            .EmailAddress()
            .WithMessage("Email is not valid.")
            .WithErrorCode("EMAIL_INVALID");

        // OrderItems list validation
        RuleFor(x => x.Order.OrderItems)
            .NotNull()
            .WithMessage("OrderItems is required.")
            .WithErrorCode("ORDERITEMS_REQUIRED")
            .Must(items => items.Any())
            .WithMessage("Order must contain at least one OrderItem.")
            .WithErrorCode("ORDERITEMS_EMPTY");

        // Each OrderItem validation
        RuleForEach(x => x.Order.OrderItems)
            .ChildRules(orderItem =>
            {
                orderItem.RuleFor(i => i.ProductId)
                    .NotEmpty()
                    .WithMessage("ProductId is required.")
                    .WithErrorCode("PRODUCT_ID_REQUIRED");

                orderItem.RuleFor(i => i.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than zero.")
                    .WithErrorCode("QUANTITY_INVALID");

                orderItem.RuleFor(i => i.Price)
                    .GreaterThan(0)
                    .WithMessage("Price must be greater than zero.")
                    .WithErrorCode("PRICE_INVALID");
            });
    }
}

