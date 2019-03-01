using CardGame.Identity;
using CardGame.Interfaces;
using CardGame.Models;
using CardGame.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame.Controllers
{
    //Using APIController allows us to not validate Model state because it is done automatically
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepo;
        private IConfiguration _configuration;

        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepo = userRepository;
            _configuration = configuration;
        }

        public async Task<IActionResult> GoogleLogin([FromBody]GoogleUserIn userIn)
        {
            await _userRepo.GoogleLogin(userIn.idToken, userIn.username, userIn.googleEmail);

            string key = _configuration.GetSection("SecretKey").ToString();
            double expireTime = Double.Parse(_configuration.GetSection("ExpireTime").ToString());
            string token = JwtHelper.CreateJwtToken(userIn.username, userIn.googleEmail, key, expireTime);

            if (token != null)
                return Ok(new
                {
                    token
                });

            return BadRequest();
        }
    }
}
