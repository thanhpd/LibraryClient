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

        public static Image FetchImage(byte[] byteArrayIn)
        {
            Image returnImage;
//            using (MemoryStream ms = new MemoryStream(byteArrayIn))
//            {                
//                returnImage = Image.FromStream(ms);                
//            }
            MemoryStream ms = new MemoryStream(byteArrayIn);
            returnImage = Image.FromStream(ms);
            returnImage.Save(ms, ImageFormat.Png);
            return returnImage;
        }
    }
}
