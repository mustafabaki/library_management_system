using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace library_management_system.Models
{
    public class Loan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsExtended { get; set; } = false;

        // Navigation properties
        public Book? Book { get; set; }
        public Member? Member { get; set; }
    }
}
