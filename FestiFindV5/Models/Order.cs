using System.ComponentModel.DataAnnotations;

namespace FestFindV2.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? EventId { get; set; }
        public Event? Event { get; set; } // Navigation property to the Event entity

        [Required]
        public bool Payed { get; set; }
    }
}
