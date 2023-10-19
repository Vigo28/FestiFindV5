using FestiFindV5.Models;
using System.ComponentModel.DataAnnotations;

namespace FestFindV2.Models
{
    public class Event
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required]
        public DateTime? Date_Time { get; set; }
        [Required]
        public float? Costs { get; set; }
        [Required]
        public int? PlacesLeft { get; set; }
        [Required]
        public int? OrganizerId { get; set; }
        public Category Category { get; set; } // Navigation property to the Category entity
        public Order order { get; set; }
        public Event()
        {

        }
        public void ReduceAvailableSpots()
        {
            if (PlacesLeft > 0)
            {
                PlacesLeft--;
            }
            else
            {
                throw new InvalidOperationException("Er zijn geen beschikbare plaatsen meer voor dit evenement.");
            }
        }
        public void IncrementAvailableSpots()
        {
                PlacesLeft++;
        }
    }
}
