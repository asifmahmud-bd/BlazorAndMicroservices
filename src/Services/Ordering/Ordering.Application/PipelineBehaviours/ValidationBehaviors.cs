using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Collections.Generic;
using ValidationException = Ordering.Application.Exceptions.ValidationException;

namespace Ordering.Application.PipelineBehaviours
{
    public class ValidationBehaviors<TRequest,TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _valudators;

        public ValidationBehaviors(IEnumerable<IValidator<TRequest>> valudators)
        {
            _valudators = valudators ?? throw new ArgumentNullException(nameof(valudators));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_valudators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResult = await Task.WhenAll(_valudators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failure = validationResult.SelectMany(e => e.Errors).Where(f => f != null).ToList();

                if(failure.Count != 0)
                {
                    throw new ValidationException(failure);
                }
            }
            return await next();
        }
    }
}
