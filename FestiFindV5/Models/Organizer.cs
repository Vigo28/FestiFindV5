using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FestFindV2.Models
{
    public class Organizer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? EMail { get; set; }
        public string? Password { get; set; }
        public ICollection<Event>? Events { get; set; } // Navigation property to the Event entity
    }
}
