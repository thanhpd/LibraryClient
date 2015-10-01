﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace LibraryDesktop
{
    public partial class SplashScreen : Telerik.WinControls.UI.RadForm
    {
        public SplashScreen()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            radWaitingBar1.StartWaiting();
        }
    }
}
