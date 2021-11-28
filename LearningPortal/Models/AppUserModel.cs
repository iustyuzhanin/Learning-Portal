using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.Domains;
using Microsoft.AspNetCore.Identity;

namespace LearningPortal.Models
{
    public class AppUserModel:IdentityUser
    {
        [Required]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Роль")]
        public string Role { get; set; }

        [Display(Name = "Курсы")]
        public List<Course> Courses { get; set; }

    }
}
