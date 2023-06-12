using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace LibraryApp.Models
{
    [Table("loan")]
    public class Loan
    {
        public int id { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int userid { get; set; }
        public User User { get; set; }
        public int bookid { get; set; }
        public Book Book { get; set; }
    }
}

