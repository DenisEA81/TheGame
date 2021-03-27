using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Surfaces;
using Rendering;

namespace KingLab
{
    public partial class FormMain : Form
    {
        public bool IsStopApplication = false;
        public IApplicationController CurrentAppController { get; set; } = null;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Maximized;
                CurrentAppController = new LabyrintGameController(new WFDrawingSurface(PictureScreen, 1, false));
                CurrentAppController.Start();
                timerAction.Enabled = true;
            }
            catch(Exception er)
            {
                MessageBox.Show($"FormMain_Load->{er.Message}");
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode.HasFlag(Keys.Enter) & e.Alt)
                {
                    CurrentAppController.Render.Surface.DevalidationSurfaces();
                    this.TopMost = !this.TopMost;
                    if (this.TopMost)
                    {
                        this.FormBorderStyle = FormBorderStyle.None;
                        this.WindowState = FormWindowState.Normal;
                        this.WindowState = FormWindowState.Maximized;
                        this.PanelScreen.BorderStyle = BorderStyle.None;
                    }
                    else
                    {
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                        this.PanelScreen.BorderStyle = BorderStyle.Fixed3D;
                    }
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show($"FormMain_KeyDown->{er.Message}");
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsStopApplication = true;
        }

        private void timerAction_Tick(object sender, EventArgs e)
        {
            timerAction.Enabled = false;
            while (!IsStopApplication)
            {
                CurrentAppController.LogicStep();
                CurrentAppController.RedrawScene();
                IsStopApplication |= CurrentAppController.ApplicationState == ApplicationStateEnum.Stop; 
                Thread.Sleep(1);
                Application.DoEvents();
            }
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.PanelScreen.BorderStyle = BorderStyle.Fixed3D;
        }
    }
}
