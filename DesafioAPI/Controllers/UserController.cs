using DesafioAPI.Models;
using DesafioAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] User user)
        {
            bool register = UserService.RegisterUser(user.Name, user.Email, user.Password);

            if (register == true)
                return Ok();
            
            return Problem();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Logar([FromBody] User user)
        {
            var checkUser = UserService.Login(user.Email, user.Password);
            if (checkUser == null)
                return NotFound();

            var token = TokenService.GenerateToken(checkUser);

            return new
            {
                user = checkUser,
                token = token
            };
        }


        [HttpPost("recoverypass")]
        [AllowAnonymous]
        public IActionResult PostRecoveryPass([FromBody] User user)
        {
            var recovery = UserService.SendEmailRecoveryPassword(user.Email);
            if (recovery == true)
                return Ok();

            return NotFound();
        }

        [HttpPost("recoverypass/{emailBase64}")]
        [AllowAnonymous]
        public ActionResult<User> GetRecoveryPass(string emailBase64)
        {
            var user = UserService.RecoveryPassLinkEmail(emailBase64);
            if (user == null)
                return NotFound();

            return user;
        }

        [HttpPut("resetpass")]
        [AllowAnonymous]
        public IActionResult UpdatePass([FromBody] User user)
        {
            bool updatePass = UserService.UpdatePassword(user.Email, user.Password);

            if (updatePass == true)
                return Ok();

            return NotFound();
        }

        [HttpGet("authenticated")]
        [Authorize]
        public ActionResult<dynamic> UserAthenticated()
        {
            return new
            {
                name = User.Identity.Name
            };
        }
    }
}
