using System.ComponentModel.DataAnnotations.Schema;
using LibraryApp.Models;

namespace LibraryApp.Models
{
    [Table("bookauthor")]
    public class BookAuthor
    {
        [Column("bookid")]
        public int bookId { get; set; }
        public Book Book { get; set; }

        [Column("authorid")]
        public int authorId { get; set; }
        public Author Author { get; set; }
    }
}
