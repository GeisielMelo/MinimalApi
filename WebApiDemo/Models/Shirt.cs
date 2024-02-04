using System.ComponentModel.DataAnnotations;
using WebApiDemo.Models.Validation;

namespace WebApiDemo.Models
{
    public class Shirt
    {
        public int Id { get; set; }

        [Required]
        public string? Brand { get; set; }

        [Required]
        public string? Color { get; set; }

        [Shirt_EnsureCorrectSizing]
        public int Size { get; set; }

        [Required]
        public string? Gender { get; set; }

        public double Price { get; set; }

    }
}
