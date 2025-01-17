﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library_management_system.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }


        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Author { get; set; }

        public bool IsAvailable { get; set; } = true;


        public ICollection<Loan>? Loans { get; set; }

    }
}
