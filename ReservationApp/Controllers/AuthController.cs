﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.BusinessUnit.Interfaces;
using ReservationApp.Dto;
using ReservationApp.Model;
using ReservationApp.Results;

namespace ReservationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusinessUnit _authBusinessUnit;
        public AuthController(IAuthBusinessUnit authBusinessUnit)
        {
            _authBusinessUnit = authBusinessUnit;
        }

        [HttpPost]
        [Route("Register")]
        public DataResult<User> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            var result = _authBusinessUnit.Register(userForRegisterDto);
            return result;
        }
        [HttpPost]
        [Route("Login")]
        public DataResult<string> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var result = _authBusinessUnit.Login(userForLoginDto);
            return result;
        }
        [HttpGet]
        [Route("GetCurrentSession")]
        public DataResult<Session> GetCurrentSession(string token)
        {
            var result = _authBusinessUnit.GetActiveSessionByToken(token);
            return result;
        }
        [Authorize]
        [HttpPost]
        [Route("Logout")]
        public Result Logout(string token)
        {
            return _authBusinessUnit.Logout(token);
        }


    }
}
