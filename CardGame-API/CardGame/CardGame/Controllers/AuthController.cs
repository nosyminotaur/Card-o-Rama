using CardGame.Identity;
using CardGame.Interfaces;
using CardGame.Models;
using CardGame.Services;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost("g-login")]
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

        [HttpPost("email-login")]
        public async Task<IActionResult> EmailLogin([FromBody]EmailUserLoginIn userIn)
        {
            var result = await _userRepo.EmailLogin(userIn.Email, userIn.Username, userIn.Password);

            if (result)
            {
                string key = _configuration.GetSection("SecretKey").ToString();
                double expireTime = Double.Parse(_configuration.GetSection("ExpireTime").ToString());
                string token = JwtHelper.CreateJwtToken(userIn.Username, userIn.Email, key, expireTime);

                if (token != null)
                    return Ok(new
                    {
                        token
                    });
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("email-register")]
        public async Task<IActionResult> EmailRegister([FromBody]EmailUserRegisterIn userIn)
        {
            var result = await _userRepo.EmailRegister(userIn.Email, userIn.Username, userIn.Name, userIn.Password);

            if (result)
                return Ok();
        
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("check-username")]
        public async Task<IActionResult> UsernameCheck([FromQuery]string username)
        {
            bool result = await _userRepo.UsernameExists(username);

            return Ok(new
            {
                Exists = result
            });
        }

        [AllowAnonymous]
        [HttpGet("email-exists")]
        public async Task<IActionResult> EmailCheck([FromQuery]string email)
        {
            bool result = await _userRepo.EmailExists(email);

            return Ok(new
            {
                Exists = result
            });
        }
    }
}
