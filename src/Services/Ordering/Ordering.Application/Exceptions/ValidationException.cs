using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Ordering.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public Dictionary<string, string[]> Errors { get;}

        public ValidationException()
            :base("Emty or null validation error has occurred")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> validationFailurs)
                    : this()
        {
            Errors = validationFailurs
                 .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                 .ToDictionary(failurGroup => failurGroup.Key, failurGroup => failurGroup.ToArray());
        }
    }
}
