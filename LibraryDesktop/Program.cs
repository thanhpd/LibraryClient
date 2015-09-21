using System;
using System.Linq;
using System.Windows.Forms;
using LibraryData.Services;

namespace LibraryDesktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DataProvider.DeleteBook("38");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RadForm1());  
        }
    }
}