using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC2.Data;
using MVC2.Models;

namespace MVC2.Controllers
{
	public class EmployeesController : Controller
	{
		ApplicationDbContext _context ;
		IWebHostEnvironment _WebHostEnvironment;

		public EmployeesController(IWebHostEnvironment web,ApplicationDbContext context)
		{
			_context = context;
			_WebHostEnvironment = web;
		}

		public IActionResult GetDetailsView(int id)
		{
			Employee employee = _context.Employees.Include(e=>e.Department).FirstOrDefault(e => e.Id == id);

			return View("Details", employee);
		}

		public IActionResult GetIndexView()
		{
			return View("Index", _context.Employees.ToList());
		}

		public IActionResult GetCreateView()
		{
			ViewBag.DeptSelectItems=new SelectList(_context.Departments.ToList(),"Id","Name");
			return View("Create");
		}

		public string GreetVisitor()
		{
			return "Welcome to MVC2!";
		}

		public string GreetUser(string name)
		{
			return $"Hi {name}\nHow are you";
		}
		[HttpGet]
		public string GetAge(string name, int birthYear)
		{
			int age = DateTime.Now.Year - birthYear;
			return $"Hi {name} \nyour age is {age}";
		}

		[HttpPost]
		public IActionResult AddNew(Employee emp, IFormFile? imageFile)
		{

			if (imageFile != null)
			{
				string imgExtension = Path.GetExtension(imageFile.FileName);
				Guid imgGuid = Guid.NewGuid();
				string imgName = imgGuid + imgExtension;
				string imgUrl = "\\Images\\" + imgName;
				emp.ImageUrl = imgUrl;

				string imgPath = _WebHostEnvironment.WebRootPath + imgUrl;

				FileStream imgstream = new FileStream(imgPath, FileMode.Create);
				imageFile.CopyTo(imgstream);
				imgstream.Dispose();
			}
			else
			{
				emp.ImageUrl = "\\Images\\No_Image.png";
			}
			/*******************************************************/
			if (emp.JoinData != null && emp.BirthDate != null)
			{
				if (((emp.JoinData - emp.BirthDate).Days / 365) < 18)
				{
					ModelState.AddModelError(string.Empty, "Illegal Hiring Age (Under 18 years old.)");
				}
			}

			if (ModelState.IsValid)
			{
				_context.Employees.Add(emp);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
			{
				ViewBag.DeptSelectItems = new SelectList(_context.Departments.ToList(), "Id", "Name");
				return View("Create", emp);
			}
		}

		[HttpGet]
		public IActionResult GetEditView(int id)
		{
			Employee emp = _context.Employees.FirstOrDefault(x => x.Id == id);
			if (emp == null)
				return NotFound();
			else
			{
				ViewBag.DeptSelectItems = new SelectList(_context.Departments.ToList(), "Id", "Name");
				return View("Edit", emp);
			}
		}

		[HttpPost]
		public IActionResult EditCurrent(Employee emp)
		{
			if (emp.JoinData != null && emp.BirthDate != null)
			{
				if (((emp.JoinData - emp.BirthDate).Days / 365) < 18)
				{
					ModelState.AddModelError(string.Empty, "Illegal Hiring Age (Under 18 years old.)");
				}
			}
			if (emp.Salary > 55000 || emp.Salary < 5500)
			{
				ModelState.AddModelError(string.Empty, "Salary must be between 5500EGP and 55000 EGP.");
			}
			if (ModelState.IsValid)
			{
				_context.Employees.Update(emp);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
			{
				ViewBag.DeptSelectItems = new SelectList(_context.Departments.ToList(), "Id", "Name");
				return View("Edit", emp);
			}
		}

		[HttpGet]
		public IActionResult GetDeleteView(int id)
		{
			Employee emp = _context.Employees.Include(e => e.Department).FirstOrDefault(x => x.Id == id);
			if (emp == null)
				return NotFound();
			else
				return View("Delete", emp);
		}

		[HttpPost]
		public IActionResult DeleteCurrent(int id)
		{
			Employee emp = _context.Employees.FirstOrDefault(x => x.Id == id);
			if (emp == null)
				return NotFound();
			else
			{
				if(emp.ImageUrl != "\\Images||No_Image.png")
				{
					string imgpath = _WebHostEnvironment.WebRootPath + emp.ImageUrl;
					if (System.IO.File.Exists(imgpath))
					{
						System.IO.File.Delete(imgpath);
					}
				}

				_context.Employees.Remove(emp);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
		}
	}
}
