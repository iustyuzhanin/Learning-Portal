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
        public string Name { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public Chapter Chapter { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Program { get; set; }

        [Required]
        public string Image { get; set; }

        public Teacher Teacher { get; set; }

        public List<Student> Students { get; set; }


    }
}
