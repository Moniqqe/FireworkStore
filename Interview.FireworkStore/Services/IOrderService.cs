using System;
using System.Collections.Generic;
using Interview.FireworkStore.Core.Domain.Entity;
using Interview.FireworkStore.Core.Dtos;
using Interview.FireworkStore.Core.Infrastructure;

namespace Interview.FireworkStore.Services
{
    public interface IOrderService
    {
        bool Create(OrderDto order);
        bool Create(IEnumerable<OrderDto> orderDtos);
        IEnumerable<Order> GetByUser(string userName);
        IEnumerable<Order> GetGroupByGuid(Guid guid);
        ValidationResult Validate(OrderDto order);
        ValidationResult Validate(IEnumerable<OrderDto> order);
    }
}
