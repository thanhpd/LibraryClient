using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDesktop.Models
{
    public class ImageSet
    {
        private static ImageSet instance = null;

        public static ImageSet GetInstance()
        {
            if (instance == null)
            {
                instance = new ImageSet();
            }

            return instance;
        }

        private ImageSet()
        {
            Images = new List<SingleImage>();
        }

        public List<SingleImage> Images { get; set; }
    }

    public class SingleImage
    {
        public string ImageUrl { get; set; }

        public Image SmallThumbnail { get; set; }

        public Image LargeThumbnail { get; set; }
    }
}
