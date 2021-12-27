using System;
using System.Collections.Generic;
using MediatR;
using Ordering.Application.Features.Oders.Responses;

namespace Ordering.Application.Features.Oders.Queries
{
    public class GetOrdersListQuery : IRequest<List<OrderResponse>>
    {
        public string UserName { get;}

        public GetOrdersListQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName)); ;
        }

       
    }
}
