using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVC2.Models
{
    public class Employee
    {
        [Display(Name="ID")]
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage ="You have to provide a valid full name.")]
        [MinLength(12,ErrorMessage ="Full name mustn't be less than 12 Characters.")]
		[MaxLength(70, ErrorMessage = "Full name mustn't exceed 70 Characters.")]
		public string FullName { get; set; }

		[DisplayName("Job")]

		[Required(ErrorMessage = "You have to provide a valid position.")]
		[MinLength(2, ErrorMessage = "position mustn't be less than 12 Characters.")]
		[MaxLength(20, ErrorMessage = "position mustn't exceed 70 Characters.")]
		public string Position { get; set; }

        [Required(ErrorMessage ="You have to provide a valid salary")]
        /*[Range(5500,55000,ErrorMessage ="Salary must be between 5500EGP and 55000 EGP.")]*/
        public decimal Salary { get; set; }

        [DisplayName("Date Of Birth")]
        public DateTime BirthDate { get; set; }

		[DisplayName("Date Of Join")]

		public DateTime JoinData { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string ImageUrl { get; set; }

        //Foriegn key
        [Range(1,int.MaxValue,ErrorMessage ="Choose a valid Department. ")]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }

		//Navigation
		[ValidateNever]

		public Department Department { get; set; }

    }
}