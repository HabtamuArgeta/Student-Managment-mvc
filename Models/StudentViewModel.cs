using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace StudentManagmrnt.Models
{
    public class StudentViewModel
    {
        
        public string? Id { get; set; }

        [Display(Name="name")]
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
        public string? PhotoUrl { get; set; }

        [Display(Name = "IsGraduated")]
        public bool IsGraduated { get; set; }


        [Display(Name = "Gender")]
        public string Gender { get; set; } 

        [Display(Name = "Age")]
        public int Age { get; set; }
    }
}


