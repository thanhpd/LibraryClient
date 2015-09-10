using System;

namespace LibraryData.Models
{
    public class BookAuthor : LibItem
    {
        public Guid BookGuid { get; set; }

        public Guid AuthorGuid { get; set; }
    }
}
