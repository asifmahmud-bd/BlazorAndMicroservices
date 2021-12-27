using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Features.Oders.Queries;
using Ordering.Application.Features.Oders.Responses;

namespace Ordering.Application.Features.Oders.Handler
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<OrderResponse>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orderlist = await _orderRepository.GetOrdersByUserName(request.UserName);

            return orderlist == null ? null : _mapper.Map<List<OrderResponse>>(orderlist);
        }
    }
}
