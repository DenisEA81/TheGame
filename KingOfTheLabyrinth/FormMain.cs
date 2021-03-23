using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Surfaces;

namespace KingLab
{
    public partial class FormMain : Form
    {
        
        public AppController Model { get; set; }

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            Model = new AppController(new SimpleGameRender(new WFDrawingSurface(PictureScreen, 1, false)));
            Model.CreateGame();
            timer1.Enabled = true;
        }

        

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.HasFlag(Keys.Enter)& e.Alt)
            {
                Model.Render.Surface.DevalidationSurfaces();
                this.TopMost = !this.TopMost;
                if (this.TopMost)
                {
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Normal;
                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                }
                e.SuppressKeyPress = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Model.RedrawScene();
        }
    }
}
