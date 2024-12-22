using ManageEmployee.Core.DTOs;
using ManageEmployee.Core.DTOs.Request;
using ManageEmployee.Core.DTOs.Response;
using ManageEmployee.Core.Interfaces;
using ManageEmployee.Core.Models;
using ManageEmployee.EF.Data.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployee.EF.Repositories
{
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly ApplicationDbContext _context;
		public DepartmentRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public IEnumerable<DepartmentDto> GetAll()
		{
			return _context.Departments
				.AsNoTracking()
				.Select(d => new DepartmentDto
				{
					Id = d.Id,
					Name = d.Name
				})
				.ToList();
		}
		public DepartmentWithEmployeeNamesDto? GetById(int id)
		{
			var department = _context.Departments
				.Include(d => d.Users)
				.AsNoTracking()
				.SingleOrDefault(d => d.Id == id);

			if (department == null)
				return null;

			var dto = new DepartmentWithEmployeeNamesDto
			{
				Id = department.Id,
				Name = department.Name,
				EmployeesNames = department.Users.Select(e => e.Name).ToList()
			};

			return dto;
		}
		public async Task Create(RequestDepartmentDto dto)
		{
			Department department = new Department()
			{
				Name = dto.Name
			};

			_context.Departments.Add(department);

			await _context.SaveChangesAsync();
		}
		public async Task<bool> Edit(int id,RequestDepartmentDto dto)
		{
			var department = await _context.Departments.FindAsync(id);

			if (department == null)
				return false;

			department.Name = dto.Name;

			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> Delete(int id)
		{
			var department = await _context.Departments.FindAsync(id);

			if (department == null)
				return false;

			_context.Departments.Remove(department);

			await _context.SaveChangesAsync();
			return true;
		}
	}
}
