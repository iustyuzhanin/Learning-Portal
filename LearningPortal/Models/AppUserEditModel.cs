using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LearningPortal.Models
{
    public class AppUserEditModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Логин")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Роль")]
        public string Role { get; set; }

        //[Required]
        //[Display(Name = "Новый пароль")]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Required]
        //[Display(Name = "Подтверждение пароля")]
        //[DataType(DataType.Password)]
        //[Compare("Password")]
        //public string ConfirmPassword { get; set; }
    }
}
