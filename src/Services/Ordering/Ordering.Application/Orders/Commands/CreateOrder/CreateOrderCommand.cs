namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        // Ensure Order object is not null
        RuleFor(x => x.Order)
            .NotNull()
            .WithMessage("Order is required")
            .WithErrorCode("ORDER_REQUIRED");

        // Apply rules only if Order is not null
        When(x => x.Order != null, () =>
        {
            // OrderName validation
            RuleFor(x => x.Order.OrderName)
                .NotEmpty()
                .WithMessage("OrderName is required")
                .WithErrorCode("ORDERNAME_REQUIRED");

            // CustomerId validation
            RuleFor(x => x.Order.CustomerId)
                .NotEmpty()
                .WithMessage("CustomerId is required")
                .WithErrorCode("CUSTOMERID_REQUIRED");

            // EmailAddress validation
            RuleFor(x => x.Order.EmailAddress)
                .NotEmpty()
                .WithMessage("EmailAddress is required")
                .WithErrorCode("EMAIL_REQUIRED")
                .EmailAddress()
                .WithMessage("Email format is invalid")
                .WithErrorCode("EMAIL_INVALID");

            // OrderItems collection validation
            RuleFor(x => x.Order.OrderItems)
                .NotNull()
                .WithMessage("OrderItems are required")
                .WithErrorCode("ORDERITEMS_REQUIRED")
                .NotEmpty()
                .WithMessage("Order must contain at least one item")
                .WithErrorCode("ORDERITEMS_EMPTY");

            // Inline validation for each order item
            RuleForEach(x => x.Order.OrderItems).ChildRules(orderItem =>
            {
                orderItem.RuleFor(i => i.ProductId)
                    .NotEmpty()
                    .WithMessage("ProductId is required")
                    .WithErrorCode("PRODUCTID_REQUIRED");

                orderItem.RuleFor(i => i.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than 0")
                    .WithErrorCode("QUANTITY_INVALID");

                orderItem.RuleFor(i => i.Price)
                    .GreaterThan(0)
                    .WithMessage("Price must be greater than 0")
                    .WithErrorCode("PRICE_INVALID");
            });
        });
    }
}


