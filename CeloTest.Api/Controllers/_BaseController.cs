using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CeloTest.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CeloTest.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult BuildResult<T>(CommonResult<T> result)
        {
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Code);
        }
    }
}