using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace LibraryDesktop
{
    public partial class LoadingForm : Telerik.WinControls.UI.RadForm
    {
        public LoadingForm()
        {
            InitializeComponent();
            radWaitingBar1.StartWaiting();
        }
    }
}
