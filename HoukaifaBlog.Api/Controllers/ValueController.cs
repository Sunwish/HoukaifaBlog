using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HoukaifaBlog.Api.Controllers
{
    [Route("api/values")]
    public class ValueController : Controller
    {
        [HttpGet] // 可选
        public IActionResult Get()
        {
            return Ok("Hello");
        }
    }
}
