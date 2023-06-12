using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    [Table("book")]
    public class Book
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }
        public int id { get; set; }
        public string title { get; set; }
        public string isbn { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}