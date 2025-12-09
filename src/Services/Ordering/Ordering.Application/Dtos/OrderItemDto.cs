using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Application.Dtos;

public record OrderItemDto(
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal Price
);

