using System;

namespace LibraryData.Models
{
    public class BookGenre : LibItem
    {
        public Guid BookGuid { get; set; }

        public Guid GenreGuid { get; set; }
    }
}
