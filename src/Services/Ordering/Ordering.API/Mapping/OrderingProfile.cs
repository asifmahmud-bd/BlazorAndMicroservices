using System;
using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Oders.Commands;

namespace Ordering.API.Mapping
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand,BasketCheckoutEvents>().ReverseMap();
        }
    }
}
