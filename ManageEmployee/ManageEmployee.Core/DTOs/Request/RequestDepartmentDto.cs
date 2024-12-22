using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageEmployee.Core.DTOs.Request
{
	public class RequestDepartmentDto
	{
		[Required(ErrorMessage = "Name cannot be Empty"),
		MinLength(2, ErrorMessage = "Name cannot be less than 2 characters"),
		MaxLength(150, ErrorMessage = "Name cannot be more than 150 characters")]
		public string Name { get; set; } = string.Empty;
	}
}
