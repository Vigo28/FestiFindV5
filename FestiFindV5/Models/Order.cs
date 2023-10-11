using System.ComponentModel.DataAnnotations;

namespace FestFindV2.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required] 
        public int? ParticipantId { get; set; }
        [Required]
        public int? EventId { get; set; }
        [Required]
        public bool Payed { get; set; }
    }
}
