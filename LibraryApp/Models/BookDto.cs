using System;
namespace LibraryApp.Models
{
    public class BookDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string isbn { get; set; }
        public List<string> authors { get; set; }
    }
}

