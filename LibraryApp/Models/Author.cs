using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    [Table("author")]
    public class Author
    {
        public Author()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }
        public int id { get; set; }
        public string name { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}

