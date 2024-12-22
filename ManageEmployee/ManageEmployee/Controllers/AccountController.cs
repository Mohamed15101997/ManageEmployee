using ManageEmployee.Core.DTOs.Request;
using ManageEmployee.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ManageEmployee.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountRepository _accountRepository;
		public AccountController(IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
		}
		[HttpPost("Register")]
		public async Task<IActionResult> Register(CreateUserDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			bool isSuccess = await _accountRepository.Register(dto);

			if (!isSuccess)
				return BadRequest();

			return Ok(new { Message = "User Registered Successfully", Data = dto });
		}
		[HttpPost("Login")]
		public async Task<IActionResult> Login(LoginUserDto dto)
		{
			if (!ModelState.IsValid)
				return Ok(new { Message = "Invalid username or password" , result = false });

			var token = await _accountRepository.Login(dto);

			if (token == null)
				return Ok(new { Message = "Invalid username or password" , result = false });

			return Ok(new
			{
				Message = "Login Successfully",
				Token = token,
				result = true,
				expiration = DateTime.Now.AddHours(1),
			});
		}
	}
}
