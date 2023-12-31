﻿using Microsoft.AspNetCore.Identity;
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
        public string? Password { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
