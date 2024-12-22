using ManageEmployee.Core.DTOs.Request;
using ManageEmployee.Core.Interfaces;
using ManageEmployee.EF.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployee.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		public UsersController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		[Authorize(Roles = "Admin,User")]
		[HttpGet]
		public ActionResult GetAll()
		{
			return Ok(_userRepository.GetAll());
		}
		[Authorize(Roles = "Admin,User")]
		[HttpGet("{id}")]
		public ActionResult GetById(string id)
		{
			var user = _userRepository.GetById(id);

			if (user == null)
				return NotFound(new { Message = "User not found" });

			return Ok(user);
		}
		[Authorize(Roles = "Admin")]
		[HttpPut("{id}")]
		public async Task<ActionResult> Edit(string id, UserDto dto)
		{
			var user = _userRepository.GetById(id);

			if (user == null)
				return NotFound(new { Message = "User not found" });

			if (!ModelState.IsValid)
				return BadRequest();

			await _userRepository.Edit(id, dto);

			return Ok(new { Message = "User Updated Successfully" });
		}
		[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(string id)
		{
			var user = _userRepository.GetById(id);

			if (user == null)
				return NotFound(new { Message = "User not found" });

			await _userRepository.Delete(id);

			return Ok(new { Message = "User Deleted Successfully" });
		}
	}
}
