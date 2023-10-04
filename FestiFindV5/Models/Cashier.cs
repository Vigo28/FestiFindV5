using System.ComponentModel.DataAnnotations;

namespace FestFindV2.Models
{
    public class Cashier
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public static void UpdateOrder(Order payed)
        {
            //If payed true
        }
    }
}
