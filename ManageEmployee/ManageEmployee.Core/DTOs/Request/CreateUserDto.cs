using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageEmployee.Core.DTOs.Request
{
	public class CreateUserDto : UserDto
	{
		[Required(ErrorMessage = "Password cannot be Empty")]
		public string Password { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password cannot be Empty")]
		[Compare("Password")]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
