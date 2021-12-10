using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.Models;
using Microsoft.AspNetCore.Http;

namespace LearningPortal.Domains
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название курса")]
        public string Name { get; set; }

        [Display(Name = "Категория")]
        public Category Category { get; set; }

        [Display(Name = "Раздел")]
        public Chapter Chapter { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Программа")]
        public string Program { get; set; }

        [Display(Name = "Изображение курса")]
        public string ImageName { get; set; }

        [NotMapped]
        [Display(Name = "Загрузить файл")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Преподаватель")]
        public Teacher Teacher { get; set; }

        [Display(Name = "Студенты")]
        public List<Student> Students { get; set; }


    }
}
