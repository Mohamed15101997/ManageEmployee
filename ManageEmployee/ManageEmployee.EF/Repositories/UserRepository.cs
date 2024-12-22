using ManageEmployee.Core.DTOs.Request;
using ManageEmployee.Core.DTOs.Response;
using ManageEmployee.Core.Interfaces;
using ManageEmployee.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployee.EF.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public UserRepository(UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public IEnumerable<GetUserDto> GetAll()
		{
			return _userManager.Users
			.AsNoTracking()
			.Where(u => u.DepartmentId != null)
			.Select(u => new GetUserDto
			{
				Id = u.Id,
				Name = u.Name,
				UserName = u.UserName,
				Salary = u.Salary,
				Address = u.Address,
				DepartmentName = u.Department.Name
			})
			.ToList();
		}
		public GetUserDto? GetById(string id)
		{
			var user = _userManager.Users
				.Include(u => u.Department)
				.AsNoTracking()
				.SingleOrDefault(u => u.Id == id);

			if (user == null)
				return null;

			var dto = new GetUserDto
			{
				Id = id,
				Name = user.Name,
				UserName = user.UserName,
				Salary = user.Salary,
				Address = user.Address,
				DepartmentName = user.Department.Name
			};

			return dto;
		}
		public async Task<bool> Edit(string id, UserDto dto)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user == null)
				return false;

			user.Name = dto.Name;
			user.UserName = dto.UserName;
			user.Address = dto.Address;
			user.Salary = dto.Salary;
			user.DepartmentId = dto.DepartmentId;

			var updateResult = await _userManager.UpdateAsync(user);

			if (!updateResult.Succeeded)
				return false;

			return true;
		}
		public async Task<bool> Delete(string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user == null)
				return false;

			var result = await _userManager.DeleteAsync(user);

			return result.Succeeded;
		}
	}
}
