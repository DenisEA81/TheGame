using System;
using System.Threading;
using System.Windows.Forms;
using Surfaces;
using ApplicationController;
using MapEditor;

namespace TheGame
{
    public partial class FormMain : Form
    {
        public bool IsStopApplication = false;
        private FormWindowState _WindowState = FormWindowState.Normal;
        public IApplicationController CurrentAppController { get; set; } = null;

        public FormMain()
        {
            InitializeComponent();        
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                CurrentAppController = new MapEditorController(new WFDrawingSurface(PictureScreen, 1, false),Application.StartupPath);
                this.Cursor = CurrentAppController.GetAppCursor();
                this.Text = CurrentAppController.Name;
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
                        _WindowState = this.WindowState;
                        this.WindowState = FormWindowState.Normal;
                        this.WindowState = FormWindowState.Maximized;
                        this.PanelScreen.BorderStyle = BorderStyle.None;
                    }
                    else
                    {
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                        this.PanelScreen.BorderStyle = BorderStyle.Fixed3D;
                        this.WindowState = _WindowState;
                    }
                    e.SuppressKeyPress = true;
                }
                SetPressedKey(new ToolLibrary.ButtonKey(e.KeyCode), true);
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
                DateTime LastDate = DateTime.Now;
                CurrentAppController.LogicStep();
                CurrentAppController.RedrawScene();
                this.Text = $"{CurrentAppController.Name}. FPS: {(1000/ DateTime.Now.Subtract(LastDate).TotalMilliseconds):f1}";
                IsStopApplication |= CurrentAppController.ApplicationState == ApplicationStateEnum.Stop; 
                Thread.Sleep(1);
                Application.DoEvents();
            }
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.PanelScreen.BorderStyle = BorderStyle.Fixed3D;
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            SetPressedKey(new ToolLibrary.ButtonKey(e.Button),true);
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            SetPressedKey(new ToolLibrary.ButtonKey(e.KeyCode), false);
        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            SetPressedKey(new ToolLibrary.ButtonKey(e.Button), false);
        }

        private void SetPressedKey(ToolLibrary.ButtonKey bkey, bool value)
        {
            if (CurrentAppController.PressedKeys.Consist(bkey))
                CurrentAppController.PressedKeys.SetValue(bkey,value);
        }

    }
}
