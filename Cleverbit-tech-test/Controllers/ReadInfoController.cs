using Cleverbit_tech_test.Messages;
using Cleverbit_tech_test.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cleverbit_tech_test.Controllers
{
   
    public class ReadInfoController : BaseAPIController
    {
        private readonly IReadInfoService _readInfoService;
        public ReadInfoController(IReadInfoService readInfoService)
        {
            _readInfoService = readInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> Initialize([FromQuery] InitializeRequest request)
        {
            var response =  await _readInfoService.InitializeDB(request);

            return new ObjectResult(response);
        }
    }
}
