using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CeloTest.Service.IService;
using CeloTest.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CeloTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // GET: api/User
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<UserModel>))]
        public async Task<IActionResult> Get([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var result = new CommonResult<IEnumerable<UserModel>>();

            result = await _service.GetAllUserAsync(firstName, lastName);

            return BuildResult(result);
        }

        // GET: api/User/{id:int}
        [HttpGet("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserModel))]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = new CommonResult<UserModel>();

            result = await _service.GetUserAsync(id);

            return BuildResult(result);
        }

        // POST: api/User
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserModel))]
        public async Task<IActionResult> Post([FromBody] [Required] UserModel model)
        {
            var result = await _service.UpdateUserAsync(model);

            return BuildResult(result);
        }

        // POST: api/User/Delete/{id:int}
        [HttpPost("Delete/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserModel))]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteUserAsync(id);

            return BuildResult(result);
        }
    }
}