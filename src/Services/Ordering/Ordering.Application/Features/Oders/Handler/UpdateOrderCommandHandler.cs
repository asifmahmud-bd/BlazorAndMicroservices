using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Application.Features.Oders.Commands;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Oders.Handler
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var item = await _orderRepository.GetByIdAsync(request.Id);

            if(item == null)
            {
                _logger.LogError("Order not exist");

                throw new NotFoundException(nameof(Order), request.Id);
            }

            _mapper.Map(request, item, typeof(UpdateOrderCommand), typeof(Order));

             await _orderRepository.UpdateAsync(item);

            _logger.LogInformation($"Order {item.Id} is successfully updated");

            return Unit.Value;

        }
    }
}
