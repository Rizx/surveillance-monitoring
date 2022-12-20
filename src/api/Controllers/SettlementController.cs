using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APIControllers
{
    [ApiController]
    [Route("/")]
    public class SettlementController : ControllerBase
    {
        private readonly ILogger<SettlementController> _logger;

        public SettlementController(
            ILogger<SettlementController> logger)
        {
            _logger = logger;
        }
    }
}
