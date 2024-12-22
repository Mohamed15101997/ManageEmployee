using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageEmployee.Core.DTOs.Response
{
	public class GetUserDto
	{
		public string Id { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public double Salary { get; set; }
		public string DepartmentName { get; set; } = string.Empty;
	}
}
