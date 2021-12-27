using System;
namespace Ordering.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object obj)
            : base($"Item {name} {obj} is not found")
        {

        }
    }
}
