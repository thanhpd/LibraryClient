using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Model
{
    public class Book : LibItem
    {        
        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Publisher { get; set; }        

        public string ImageUrl { get; set; }

        public int TotalPages { get; set; }

        public string Status { get; set; }

        public string Language { get; set; }

        public string Summary { get; set; }

        public string LocationCode { get; set; }

        public virtual List<Author> Authors { get; set; }

        public virtual List<Genre> Genres { get; set; }
    }
}
