using System.ComponentModel.DataAnnotations;

namespace LearningPortal.Domains
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public string Name { get; set; }
    }
}
