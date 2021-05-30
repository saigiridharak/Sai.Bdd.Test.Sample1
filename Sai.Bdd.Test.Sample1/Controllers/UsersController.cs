using Microsoft.AspNetCore.Mvc;
using Sai.Bdd.Test.Sample1.Models;
using Sai.Bdd.Test.Sample1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sai.Bdd.Test.Sample1.Controllers
{
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IList<User>> GetUsers([FromQuery]int pageNo)
        {
            return await _service.GetUsers(pageNo);
        }
    }
}
