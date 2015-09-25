using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using LibraryData.Utils;
using LibraryDesktop.Models;
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

        public static Image AddImageStore(string imagePath, byte[] byteArrayIn)
        {
            var listImages = ImageSet.GetInstance();

            var smallThumb = FetchImage(byteArrayIn, 150, 80);
            var largeThumb = FetchImage(byteArrayIn, 250, 150);
            var singleImage = new SingleImage
            {
                ImageUrl = imagePath,
                SmallThumbnail = smallThumb,
                LargeThumbnail = largeThumb
            };

            listImages.Images.Add(singleImage);

            return smallThumb;
        }

        public static Image FetchLargeThumb(string imagePath)
        {
            var listImages = ImageSet.GetInstance();
            var image = listImages.Images.Where(t => t.ImageUrl == imagePath).ToList().First().LargeThumbnail;

            return image;
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

        public static Image FetchImage(string imagePath, int maxWidth, int maxHeight)
        {
            Image finalImage;
            using (var image = Image.FromFile(imagePath))
            {
                finalImage = ScaleImage(image, maxWidth, maxHeight);                
            }

            return finalImage;
        }

        public static Image FetchImage(Image image, int maxWidth, int maxHeight)
        {
            return ScaleImage(image, maxWidth, maxHeight);
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

        public static bool IsConnectedToInternet
        {
            get
            {
                Uri url = new Uri(UrlBuilder.BaseUrl);
                string pingurl = string.Format("{0}", url.Host);
                string host = pingurl;
                bool result = false;
                Ping p = new Ping();
                try
                {
                    PingReply reply = p.Send(host, 3000);
                    if (reply.Status == IPStatus.Success)
                        return true;
                }
                catch { }
                return result;
            }
        }
    }
}
