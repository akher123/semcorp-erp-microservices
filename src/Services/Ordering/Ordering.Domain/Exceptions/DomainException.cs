using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Exceptions;

public class DomainException:Exception
{
    public DomainException()
    {
    }
    public DomainException(string message) : base($"Domain Exception:\"{message}\" Throws Domain Layer.")
    {
    }
 
}
