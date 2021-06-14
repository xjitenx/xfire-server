using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xfire_server.Models;
using xfire_server.Services;

namespace xfire_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {        
        [HttpGet("getUsers")]
        public List<User> getUsers()
        {
            UserServices userServices = new UserServices();
            return userServices.getUsers();
        }

        [HttpPost("registerUser")]
        public List<User> registerUser([FromBody]User userDetails)
        {
            UserServices userServices = new UserServices();
            userServices.registerUser(userDetails);
            var users = getUsers();
            return users;
        }

        [HttpPost("login")]
        public string loginUser([FromBody]Login loginDetails)
        {
            if (getUsers().FindIndex((user) => (user.UserEmailId == loginDetails.LoginId || user.UserAlias == loginDetails.LoginId) && user.UserPassword == loginDetails.Password) != -1)
            {
                return "Success!";
            }
            return "Failed! Try Again!";
        }
    }
}
