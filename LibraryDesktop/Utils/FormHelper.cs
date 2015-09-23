using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;

namespace LibraryDesktop.Utils
{
    public class FormHelper
    {
        public static void SetTextForTextBox(RadTextBox textBox, RadGridView gridView, string cellId)
        {
            try
            {
                textBox.Text = gridView.SelectedRows[0].Cells[cellId].Value.ToString();
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.Message);
            }
            
        }

        public static void SetTextForTextBox(RadTextBox textBox, RadGridView gridView, int cellId)
        {
            textBox.Text = gridView.SelectedRows[0].Cells[cellId].Value.ToString();
        }

        public static Image FetchImage(byte[] byteArrayIn, int maxWidth, int maxHeight)
        {
            Image finalImage;            
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                using (var returnImage = Image.FromStream(ms))
                {
                    finalImage = ScaleImage(returnImage, maxWidth, maxHeight);
                    finalImage.Save(ms, ImageFormat.Png);
                }                
            }
            
            return finalImage;
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
    }
}
