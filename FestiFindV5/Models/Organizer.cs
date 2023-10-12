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

        public static void CreateEvent()
        {
            //Create event
        }
        public static void UpdateEvent(Event eventId)
        {
            //Update event information
        }
        public static void DeleteEvent(Event eventId)
        {
            //Delete entire event
        }
        public static void CreateCashier()
        {
            //Create event
        }
        public static void UpdateCashier(Cashier cashierId)
        {
            //Update Cashier with Id
        }
        public static void DeleteCashier(Cashier cashierId)
        {
            //Delete Cashier with Id
        }
    }
}
