using ManageEmployee.Core.DTOs;
using ManageEmployee.Core.DTOs.Request;
using ManageEmployee.Core.DTOs.Response;
using ManageEmployee.Core.Interfaces;
using ManageEmployee.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployee.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentsController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		public DepartmentsController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork; 
		}
		[Authorize(Roles = "Admin,User")]
		[HttpGet]
		public ActionResult GetAll()
		{
			return Ok(_unitOfWork.Department.GetAll());
		}
		[Authorize(Roles = "Admin,User")]
		[HttpGet("{id}")]
		public ActionResult GetById(int id)
		{
			var department = _unitOfWork.Department.GetById(id);

			if (department == null)
				return NotFound(new { Message = "Department not found" });

			return Ok(department);
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<ActionResult> Add(RequestDepartmentDto dto)
		{
			if (dto == null)
				return BadRequest();

			await _unitOfWork.Department.Create(dto);
			return Ok(new { Message = "Department Added Successfully", Data = dto });
		}
		[Authorize(Roles = "Admin")]
		[HttpPut("{id}")]
		public async Task<ActionResult> Edit(int id, RequestDepartmentDto dto)
		{
			var department = _unitOfWork.Department.GetById(id);

			if(department == null)
				return NotFound(new { Message = "Department not found"});

			if (!ModelState.IsValid)
				return BadRequest();

			await _unitOfWork.Department.Edit(id, dto);

			return Ok(new { Message = "Department Updated Successfully" });
		}
		[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var department = _unitOfWork.Department.GetById(id);

			if (department == null)
				return NotFound(new { Message = "Department not found" });

			 await _unitOfWork.Department.Delete(id);

			return Ok(new { Message = "Department Deleted Successfully" });
		}

	}
}
