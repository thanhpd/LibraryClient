using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Model
{
    public class BookAuthor : LibItem
    {
        public Guid BookGuid { get; set; }

        public Guid AuthorGuid { get; set; }
    }
}
