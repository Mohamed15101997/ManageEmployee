using ManageEmployee.Core.DTOs.Request;
using ManageEmployee.Core.Interfaces;
using ManageEmployee.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManageEmployee.EF.Repositories
{
	public class AccountRepository : IAccountRepository
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;
		public AccountRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<bool> Register(CreateUserDto dto)
		{
			ApplicationUser appUser = new ApplicationUser()
			{
				Name = dto.Name,
				UserName = dto.UserName,
				PasswordHash = dto.Password,
				Address = dto.Address,
				Salary = dto.Salary,
				DepartmentId = dto.DepartmentId
			};

			IdentityResult result = await _userManager.CreateAsync(appUser,dto.Password);
			if (result.Succeeded)
			{
				var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

				if (roleResult.Succeeded)
					return true;
				else
					return false;
			}
			else
				return false;
		}
		public async Task<string?> Login(LoginUserDto dto)
		{
			var appUser = await _userManager.FindByNameAsync(dto.UserName);
			if (appUser == null)
				return null;

			var isPasswordValid = await _userManager.CheckPasswordAsync(appUser, dto.Password);
			if (!isPasswordValid)
				return null;
			var token = await GenerateJwtToken(appUser, appUser.Id);

			return token;
		}

		public async Task<string> GenerateJwtToken(ApplicationUser appUser, string userId)
		{
			List<Claim> userClaims = new List<Claim>();
			var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecritKey"]));
			userClaims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id));
			userClaims.Add(new Claim(ClaimTypes.Name, appUser.UserName));
			userClaims.Add(new Claim("UserName", appUser.Name ?? string.Empty));
			var userRole = await _userManager.GetRolesAsync(appUser);
			foreach (var item in userRole)
			{
				userClaims.Add(new Claim(ClaimTypes.Role, item));
			}
			userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
			var signingCred = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
			JwtSecurityToken loginToken = new JwtSecurityToken
				(
				issuer: _configuration["JWT:IssuerIP"],
				audience: _configuration["JWT:AudienceIP"],
				expires: DateTime.Now.AddHours(1),
				claims: userClaims,
				signingCredentials: signingCred
				);

			var tokenHandler = new JwtSecurityTokenHandler();
			string token = tokenHandler.WriteToken(loginToken);

			return token;
		}
	}
}
