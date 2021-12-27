using System;
using MediatR;

namespace Ordering.Application.Features.Oders.Commands
{
    public class DeleteOrderCommand : IRequest
    {
        public int Id { get; set; }
    }
}
