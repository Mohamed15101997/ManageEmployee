using ManageEmployee.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageEmployee.EF.Data.DataSeed
{
	public static class SeedData
	{
		public static async Task InitializeAsync(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			// Add roles
			if (!await roleManager.RoleExistsAsync("Admin"))
				await roleManager.CreateAsync(new IdentityRole("Admin"));

			if (!await roleManager.RoleExistsAsync("User"))
				await roleManager.CreateAsync(new IdentityRole("User"));

			// Add admin user
			var adminEmail = "admin@gmail.com";
			var adminUserName = "admin97";
			var adminName = "Mohamed";
			var adminAddress = "Giza";
			var adminSalary = 50000;
			var adminUser = await userManager.FindByEmailAsync(adminEmail);
			if (adminUser == null)
			{
				adminUser = new ApplicationUser
				{
					Name = adminName,
					UserName = adminUserName,
					Email = adminEmail,
					EmailConfirmed = true,
					Address = adminAddress,
					Salary = adminSalary,
					DepartmentId = null
				};

				var result = await userManager.CreateAsync(adminUser, "Admin@123");
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(adminUser, "Admin");
				}
			}
		}
	}
}
