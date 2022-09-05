using Project2_32210310.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project2_32210310.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class AuthenticateController
    {


        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)

        {

            this.userManager = userManager;

            this.roleManager = roleManager;

            _configuration = configuration;

        }
       // [HttpPost]

        //[Route("login")]
        //public async Task<IActionResult> Login([FromBody] LoginModel model)
        //{

        //}


    }
}
