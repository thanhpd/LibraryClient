using System;
using System.Collections.Generic;

namespace LibraryData.Models
{
    public class Book
    {        
        public string id { get; set; }

        public string book_image { get; set; }

        public string book_name { get; set; }

        public string book_author { get; set; }

        public string book_publisher { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public string book_year { get; set; }

        public string status { get; set; }

        public string book_description { get; set; }
    }
}
