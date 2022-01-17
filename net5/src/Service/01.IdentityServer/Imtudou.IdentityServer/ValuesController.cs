using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> logger;
        private readonly EventId @event;
        public ValuesController(ILogger<ValuesController> logger, EventId eventId)
        {
            this.logger = logger;
            @event = eventId;
        }

        public (bool a, string b) GetResult(int a, string b)
        {
            logger.LogInformation(@event.Id, "GetResult", new { a = a, b = b });
            return (a > 0, b);       
        }
    }
}
