
namespace KingLab
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.PanelScreen = new System.Windows.Forms.Panel();
            this.PictureScreen = new System.Windows.Forms.PictureBox();
            this.timerAction = new System.Windows.Forms.Timer(this.components);
            this.PanelScreen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelScreen
            // 
            this.PanelScreen.BackColor = System.Drawing.SystemColors.Control;
            this.PanelScreen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelScreen.Controls.Add(this.PictureScreen);
            this.PanelScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelScreen.Location = new System.Drawing.Point(0, 0);
            this.PanelScreen.Name = "PanelScreen";
            this.PanelScreen.Size = new System.Drawing.Size(504, 504);
            this.PanelScreen.TabIndex = 0;
            // 
            // PictureScreen
            // 
            this.PictureScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureScreen.ErrorImage = null;
            this.PictureScreen.Location = new System.Drawing.Point(0, 0);
            this.PictureScreen.MinimumSize = new System.Drawing.Size(500, 500);
            this.PictureScreen.Name = "PictureScreen";
            this.PictureScreen.Size = new System.Drawing.Size(500, 500);
            this.PictureScreen.TabIndex = 0;
            this.PictureScreen.TabStop = false;
            // 
            // timerAction
            // 
            this.timerAction.Interval = 200;
            this.timerAction.Tick += new System.EventHandler(this.timerAction_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 504);
            this.Controls.Add(this.PanelScreen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(520, 543);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Король лабиринтов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.PanelScreen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelScreen;
        private System.Windows.Forms.PictureBox PictureScreen;
        private System.Windows.Forms.Timer timerAction;
    }
}

