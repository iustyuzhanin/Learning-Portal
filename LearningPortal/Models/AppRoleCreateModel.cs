using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPortal.Models
{
    public class AppRoleCreateModel
    {
        [Required]
        [Display(Name = "Название роли")]
        public string RoleName { get; set; }
    }
}
