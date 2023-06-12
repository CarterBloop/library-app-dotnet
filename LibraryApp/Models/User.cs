using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    [Table("user")]
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public ICollection<Loan> Loans { get; set; } // Add a list of loans
    }
}