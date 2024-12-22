using ManageEmployee.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageEmployee.Core.DTOs.Request
{
	public class UserDto
	{
		[Required(ErrorMessage = "Name cannot be Empty"),
		MinLength(3, ErrorMessage = "Name cannot be less than 3 characters"),
		MaxLength(150, ErrorMessage = "Name cannot be more than 150 characters")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "User Name cannot be Empty")]
		[Display(Name = "User Name")]
		public string UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Address cannot be Empty"),
		MinLength(3, ErrorMessage = "Address cannot be less than 3 characters"),
		MaxLength(250, ErrorMessage = "Address cannot be more than 250 characters")]
		public string Address { get; set; } = string.Empty;

		[Required(ErrorMessage = "Salary cannot be Empty"),
		Range(1, 50000, ErrorMessage = "Salary Must be between 1 and 50000")]
		public double Salary { get; set; }

		[Required(ErrorMessage = "Department cannot be Empty")]
		public int? DepartmentId { get; set; }
	}
}
