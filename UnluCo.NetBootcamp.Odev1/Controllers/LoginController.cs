using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.Entities.Concrete;
using UnluCo.NetBootcamp.Odev5.Models;
using UnluCo.NetBootcamp.Odev5.Services;

namespace UnluCo.NetBootcamp.Odev5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TokenGenerator _tokenGenerator;
        public LoginController(TokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginUserModel user)
        {
            Token token = new Token();
            if (user.Password == "12345" && user.Username == "User")
            {
                _tokenGenerator.CreateToken(user);
                return Ok(token);
            }

            return Unauthorized();
        }
    }
}
