namespace Ordering.Application.Dtos;

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    string EmailAddress,
    List<OrderItemDto> OrderItems
);
