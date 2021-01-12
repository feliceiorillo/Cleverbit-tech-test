using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cleverbit_tech_test.Controllers
{
   
    public class PingController : BaseAPIController
    {
        private readonly IWebHostEnvironment _env;
        public PingController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok($"I am working on the: {_env.EnvironmentName}");
        }
    }
}
