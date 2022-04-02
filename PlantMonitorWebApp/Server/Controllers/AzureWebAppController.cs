using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace AzureWebAppTest.Controllers
{
    [ApiController]
    [Route("AzureWebAppTest")]
    public class AzureWebAppTestController : Controller
    {
        private readonly IConfiguration configuration;

        public AzureWebAppTestController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("")]
        [HttpGet("index")]
        [HttpGet("Get")]
        public IActionResult Index()
        {
            var connectionStringTest1 = configuration.GetConnectionString("PlantWebDb");
            return Ok(connectionStringTest1);
        }

    }
}