using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC2.Data;
using MVC2.Models;
using System.Security.Cryptography;

namespace MVC2.Controllers
{
	public class DepartmentsController : Controller
	{
		ApplicationDbContext _context;
        public DepartmentsController(ApplicationDbContext context)
		{
			_context = context;
		}
        public IActionResult GetDetailsView(int id)
		{
			Department Dept = _context.Departments.Include(d=>d.Employees).FirstOrDefault(e => e.Id == id);
			return View("Details", Dept);
		}
		public IActionResult GetIndexView()
		{
			return View("Index", _context.Departments.ToList());
		}
		public IActionResult GetCreateView()
		{
			return View("Create");
		}
		public IActionResult AddNew(Department dept)
		{
			if (ModelState.IsValid)
			{
				_context.Departments.Add(dept);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
				return View("Create", dept);
		}

		[HttpGet]
		public IActionResult GetEditView(int id)
		{
			Department dept = _context.Departments.FirstOrDefault(x => x.Id == id);
			if (dept == null)
				return NotFound();
			else
				return View("Edit", dept);
		}

		[HttpPost]
		public IActionResult EditCurrent(Department dept)
		{
			if (ModelState.IsValid)
			{
				_context.Departments.Update(dept);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
				return View("Edit", dept);
		}

		[HttpGet]
		public IActionResult GetDeleteView(int id)
		{
			Department dept = _context.Departments.FirstOrDefault(x => x.Id == id);
			if (dept == null)
				return NotFound();
			else
				return View("Delete", dept);
		}

		[HttpPost]
		public IActionResult DeleteCurrent(int id)
		{
			Department dept = _context.Departments.FirstOrDefault(x => x.Id == id);
			if (dept == null)
				return NotFound();
			else
			{
				_context.Departments.Remove(dept);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
		}
	}
}
