using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Interview.FireworkStore.Core.Domain.Entity;
using Interview.FireworkStore.Core.Domain.Interfaces;
using Interview.FireworkStore.Core.Dtos;
using Interview.FireworkStore.Core.Infrastructure;

namespace Interview.FireworkStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IDataWriter<Order> _dataWriter;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IFireworkService _fireworkService;

        private readonly ICollection<Order> _orders;

        public OrderService(IDataReader dataReader, IDataWriter<Order> dataWriter, IMapper mapper, IUserService userService, IFireworkService fireworkService)
        {
            _orders = new HashSet<Order>(dataReader.LoadOrders());
            _dataWriter = dataWriter;
            _mapper = mapper;
            _userService = userService;
            _fireworkService = fireworkService;
        }

        public bool Create(OrderDto orderDto)
        {
            var id = _orders.Any() ? _orders.Max(order => order.Id) + 1 : 1;
            var order = _mapper.Map<Order>(orderDto);
            order.Id = id;
            order.Created = DateTime.Now.Date;
            order.GroupId = Guid.NewGuid();

            var success = _dataWriter.Create(order);

            if (success)
            {
                _orders.Add(order);
            }

            return success;
        }

        public bool Create(IEnumerable<OrderDto> orderDtos)
        {
            var groupId = Guid.NewGuid();
            var created = DateTime.Now.Date;

            var orders = _mapper.Map<IEnumerable<Order>>(orderDtos).ToList();

            var id = _orders.Any() ? _orders.Max(order => order.Id) + 1 : 1;

            for (var i = 0; i < orders.Count; i++)
            {
                orders[i].GroupId = groupId;
                orders[i].Created = created;
                orders[i].Id = id+i;
            }

            var success = _dataWriter.Create(orders);

            if (success)
            {
                foreach (var order in orders)
                {
                    _orders.Add(order);
                }
            }

            return success;
        }

        public IEnumerable<Order> GetGroupByGuid(Guid guid)
        {
            return _orders.Where(order => order.GroupId == guid);
        }

        public IEnumerable<Order> GetByUser(string userName)
        {
            return _orders.Where(order => order.UserName.Equals(userName));
        }

        public ValidationResult Validate(IEnumerable<OrderDto> orders)
        {
            var results = new List<ValidationResult>();
            foreach (var order in orders)
            {
                results.Add(Validate(order));
            }

            var errors = results.SelectMany(r => r.Errors).Distinct();

            var result = new ValidationResult();
            foreach (var error in errors)
            {
                result.AddError(error);
            }

            return result;
        }

        public ValidationResult Validate(OrderDto order)
        {
            var result = new ValidationResult();
            var users = _userService.GetByName(order.UserName);

            if (!users.Any())
            {
                result.AddError($"User {order.UserName} was not found.");
            }

            var firework = _fireworkService.GetById(order.FireworkId);
            if (firework is null)
            {
                result.AddError($"Firework ID {order.FireworkId} was not found.");
            }
            else if (firework.Quantity < order.Quantity)
            {
                result.AddError("Order quantity exceeds the available amount.");
            }

            return result;
        }
    }
}
