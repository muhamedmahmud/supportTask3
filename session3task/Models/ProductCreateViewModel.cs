using System.ComponentModel.DataAnnotations;

namespace session3task.Models
{
    public class ProductCreateViewModel
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string CompanyName { get; set; }
    }
}