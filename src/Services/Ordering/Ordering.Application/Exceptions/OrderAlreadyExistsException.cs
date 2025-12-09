using BuildingBlock.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ordering.Application.Exceptions;

public class OrderAlreadyExistsException : DuplicateRequestException
{
    public OrderAlreadyExistsException(string message,string details) : base(message, details)
    {

    }
}
