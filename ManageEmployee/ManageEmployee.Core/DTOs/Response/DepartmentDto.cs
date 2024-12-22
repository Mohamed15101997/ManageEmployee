using ManageEmployee.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace ManageEmployee.Core.DTOs.Response
{
	public class DepartmentDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
	}
}
