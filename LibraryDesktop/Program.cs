using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using LibraryData.Services;
using Telerik.WinControls.UI;

namespace LibraryDesktop
{
    static class Program
    {
        public static SplashScreen splashForm = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            
            //show splash
            Thread splashThread = new Thread(new ThreadStart(
                delegate
                {
                    splashForm = new SplashScreen();
                    Application.Run(splashForm);
                }
                ));

            splashThread.SetApartmentState(ApartmentState.STA);
            splashThread.Start();

            //run form - time taking operation
            RadForm2 mainForm = new RadForm2();
            mainForm.Load += new EventHandler(radForm2_Load);
            Application.Run(mainForm);            
        }

        private static void radForm2_Load(object sender, EventArgs e)
        {
            //close splash
            if (splashForm == null)
            {
                return;
            }

            splashForm.Invoke(new Action(splashForm.Close));
            splashForm.Dispose();
            splashForm = null;
        }
    }
}