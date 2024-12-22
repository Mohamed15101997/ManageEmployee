using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageEmployee.Core.DTOs.Request
{
	public class LoginUserDto
	{
		[Required(ErrorMessage = "User Name cannot be Empty")]
		[Display(Name = "User Name")]
		public string UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password cannot be Empty")]
		public string Password { get; set; } = string.Empty;
	}
}
