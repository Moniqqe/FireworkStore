using AutoMapper;
using Interview.FireworkStore.Core.Domain.Entity;
using Interview.FireworkStore.Core.Dtos;
using Interview.FireworkStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Interview.FireworkStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FireworkController : ControllerBase
    {
        private readonly ILogger<FireworkController> _logger;
        private readonly IFireworkService _service;
        private readonly IMapper _mapper;

        public FireworkController(ILogger<FireworkController> logger, IFireworkService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FireworkDto>> Get()
        {
            var fireworks = _service.GetAll();

            var dtos = _mapper.Map<IEnumerable<FireworkDto>>(fireworks);

            if (dtos.Any())
            {
                return Ok(dtos);
            }
            _logger.LogError("Action get returned empty object");
            return NoContent();
        }
    }
}
