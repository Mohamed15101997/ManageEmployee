using System.ComponentModel.DataAnnotations;

namespace ManageEmployee.Core.Models
{
	public class Department
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Name cannot be Empty"),
		MinLength(2, ErrorMessage = "Name cannot be less than 2 characters"),
		MaxLength(150, ErrorMessage = "Name cannot be more than 150 characters")]
		public string Name { get; set; } = string.Empty;
		public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
	}
}
