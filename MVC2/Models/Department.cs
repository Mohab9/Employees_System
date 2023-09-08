using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC2.Models
{
	public class Department
	{
		public int Id { get; set; }


		//[Display(Name=("Name")]
		[DisplayName("Name")]
		[Required(ErrorMessage = "You have to provide a valid name.")]
		[MinLength(1, ErrorMessage = "Name mustn't be less than 12 characters.")]
		[MaxLength(70, ErrorMessage = "Name mustn't exceed 70 characters.")]
		public string Name { get; set; }


		[DisplayName("Description")]
		[Required(ErrorMessage = "You have to provide a valid description.")]
		[MinLength(2, ErrorMessage = "Description mustn't be less than 2 characters.")]
		[MaxLength(200, ErrorMessage = "Description mustn't exceed 20 characters.")]

		public string Description { get; set; }

		//Navigation prop
		[ValidateNever]
		public List<Employee> Employees { get; set; }

    }
}


