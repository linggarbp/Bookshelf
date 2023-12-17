using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        public int YearRelease { get; set; }
        [Required]
        public string? Publisher { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Pages { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public bool IsBestSeller { get; set; }
        [Required]
        public string? Image { get; set; }
    }
}
