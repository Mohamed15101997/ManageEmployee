using ManageEmployee.Core.DTOs.Request;
using ManageEmployee.Core.DTOs.Response;

namespace ManageEmployee.Core.Interfaces
{
	public interface IUserRepository
	{
		IEnumerable<GetUserDto> GetAll();
		GetUserDto? GetById(string id);
		Task<bool> Edit(string id, UserDto dto);
		Task<bool> Delete(string id);
	}
}
