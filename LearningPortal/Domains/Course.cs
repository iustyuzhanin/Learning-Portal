using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.Models;

namespace LearningPortal.Domains
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название курса")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Раздел")]
        public Chapter Chapter { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Программа")]
        public string Program { get; set; }

        [Required]
        [Display(Name = "Изображение курса")]
        public string Image { get; set; }

        [Display(Name = "Преподаватель")]
        public Teacher Teacher { get; set; }

        [Display(Name = "Студенты")]
        public List<Student> Students { get; set; }


    }
}
