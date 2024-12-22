using ManageEmployee.Core.DTOs.Request;
using ManageEmployee.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageEmployee.Core.Interfaces
{
	public interface IAccountRepository
	{
		Task<bool> Register(CreateUserDto dto);
		Task<string?> Login(LoginUserDto dto);
		Task<string> GenerateJwtToken(ApplicationUser appUser, string userId);
	}
}
