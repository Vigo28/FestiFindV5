using FestFindV2.Models;
using System.ComponentModel.DataAnnotations;

namespace FestiFindV5.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public byte[]? Image { get; set; }
        public ICollection<Event>? Events { get; set; } // Navigation property to the Event entity
    }
}
