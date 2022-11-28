using Interview.FireworkStore.Core.Domain.Entity;
using Interview.FireworkStore.Core.Dtos;
using Interview.FireworkStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interview.FireworkStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _service;

        public OrderController(ILogger<OrderController> logger, IOrderService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{userName}")]
        public ActionResult<IEnumerable<Order>> Get([FromRoute]string userName)
        {
            var results = _service.GetByUser(userName);
            if (results.Any())
            {
                return Ok(results);
            }

            _logger.LogError("Action get returned empty object");
            return NoContent();
        }

        [HttpGet("Group/{guid}")]
        public ActionResult<IEnumerable<Order>> GetGroup([FromRoute]Guid guid)
        {
            var results = _service.GetGroupByGuid(guid);
            if (results.Any())
            {
                return Ok(results);
            }
            _logger.LogError("Action getGroup returned empty object");
            return NoContent();
        }

        [HttpPost]
        public ActionResult Create([FromBody] OrderDto orderDto)
        {
            var validationResult = _service.Validate(orderDto);

            if (!validationResult.Success)
            {
                return BadRequest(validationResult.Errors.FirstOrDefault());
            }

            if (_service.Create(orderDto))
            {
                return Created($"{orderDto.UserName}", orderDto);
            }
            _logger.LogError("Action create order failed");
            return BadRequest();
        }

        [HttpPost("Group")]
        public ActionResult Create([FromBody] IEnumerable<OrderDto> orderDtos)
        {
            var validationResult = _service.Validate(orderDtos);

            if (!validationResult.Success)
            {
                return BadRequest(validationResult.Errors.FirstOrDefault());
            }

            if (_service.Create(orderDtos))
            {
                return Created($"{orderDtos.First().UserName}", orderDtos);
            }
            _logger.LogError("Action create orders failed");
            return BadRequest();
        }
    }
}