using AutoMapper;
using Interview.FireworkStore.Core.Domain.Entity;
using Interview.FireworkStore.Core.Dtos;

namespace Interview.FireworkStore.Core.Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Firework, FireworkDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
        }
    }
}
