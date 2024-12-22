using ManageEmployee.Core.Interfaces;
using ManageEmployee.EF.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageEmployee.EF.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		public IDepartmentRepository Department { get; private set; }


		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			Department = new DepartmentRepository(_context);
		}
		public int Complete() 
		{
			return _context.SaveChanges();
		}
		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
