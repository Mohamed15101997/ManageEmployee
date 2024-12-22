using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageEmployee.Core.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IDepartmentRepository Department { get; }
		int Complete();
	}
}
