using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketSupportSystem.Data.Entities;
using TicketSupportSystem.DTOs.Requests;
using TicketSupportSystem.Interfaces;

namespace TicketSupportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IJwtTokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegistrationDTO userRegDTO)
        {

            var user = _mapper.Map<UserRegistrationDTO, User>(userRegDTO);
            var result = await _userManager.CreateAsync(user, userRegDTO.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(user.Id);
            }

            return BadRequest(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {

            var user = await _userManager.FindByNameAsync(userLoginDTO.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, userLoginDTO.Password))
            {

                var token = _tokenService.GenerateJwtToken(user);
                return Ok(new { token = token.Result });
            }
            return Unauthorized();
        }


    }
}
