using ManageEmployee.Core.DTOs.Request;
using ManageEmployee.Core.DTOs.Response;
using ManageEmployee.Core.Models;
namespace ManageEmployee.Core.Interfaces
{
	public interface IDepartmentRepository
	{
		IEnumerable<DepartmentDto> GetAll();
		DepartmentWithEmployeeNamesDto? GetById(int id);
		Task Create(RequestDepartmentDto dto);
		Task<bool> Edit(int id,RequestDepartmentDto dto);
		Task<bool> Delete(int id);
	}
}
