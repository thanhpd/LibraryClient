using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDesktop.Models
{
    public class PostBookModel
    {
        [DisplayName("Book Name")]
        public string book_name { get; set; }

        [DisplayName("Description")]
        public string book_description { get; set; }

        [DisplayName("Author(s)")]
        public string book_author { get; set; }

        [DisplayName("Publisher")]
        public string book_publisher { get; set; }

        [DisplayName("Year")]
        public string book_year { get; set; }

        [DisplayName("Image Path")]
        public string image_path { get; set; }

        [DisplayName("Preview")]
        public Image image
        {
            get
            {
                return Image.FromFile(Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    "Resources\\img-placeholder.PNG"));
            }

            set { image = value; }
        }
    }
}
