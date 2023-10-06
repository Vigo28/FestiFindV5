using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FestFindV2.Models
{
    public class Participant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? EMail { get; set; }
        [Required]

        public string? Password { get; set; }
        public List<Order>? Orders { get; set; } = new List<Order>();

        public static void ShowTickets(Order orderId)
        {
            //tickets weergeven
        }
        public static void ReserveTicket(Event eventId)
        {
            //create order by chosen eventId
        }
        public static void DeleteTicket(Order orderId)
        {
            //Delete ticket by orderId
        }
    }
}
