using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsServiceTemplate
{
    public partial class MyServiceTemplate : ServiceBase
    {
        private bool msgProcRunning = false;
        private bool msgProcPaused = false;

        public MyServiceTemplate()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (!msgProcRunning)
            {
                msgProcRunning = true;
                if (msgProcPaused)
                {
                    Thread.Sleep(500);
                }
                //other code goes here.
            }
        }

        protected override void OnStop()
        {
            if (msgProcRunning)
            {
                msgProcRunning = false;
            }
        }

        protected override void OnPause()
        {
            msgProcPaused = true;
            base.OnPause();
        }
        protected override void OnContinue()
        {
            msgProcPaused = false;
            base.OnContinue();
        }
    }
}
