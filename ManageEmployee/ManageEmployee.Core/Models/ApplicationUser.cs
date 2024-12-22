using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace ManageEmployee.Core.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required(ErrorMessage = "Name cannot be Empty"),
		MinLength(3, ErrorMessage = "Name cannot be less than 3 characters"),
		MaxLength(150, ErrorMessage = "Name cannot be more than 150 characters")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Address cannot be Empty"),
		MinLength(3, ErrorMessage = "Address cannot be less than 3 characters"),
		MaxLength(250, ErrorMessage = "Address cannot be more than 250 characters")]
		public string Address { get; set; } = string.Empty;

		[Required(ErrorMessage = "Salary cannot be Empty"),
		Range(1, 50000, ErrorMessage = "Salary Must be between 1 and 50000")]
		public double Salary { get; set; }

		[ForeignKey("Department")]
		public int? DepartmentId { get; set; }
		public Department? Department { get; set; } = default!;

	}
}
