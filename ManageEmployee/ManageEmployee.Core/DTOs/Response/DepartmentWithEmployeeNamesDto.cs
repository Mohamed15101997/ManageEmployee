using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageEmployee.Core.DTOs.Response
{
	public class DepartmentWithEmployeeNamesDto : DepartmentDto
	{
		public List<string> EmployeesNames { get; set; } = new List<string>();
	}
}
