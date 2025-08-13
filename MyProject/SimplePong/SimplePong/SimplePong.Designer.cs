using System.Drawing;
using System.Windows.Forms;

namespace SimplePong
{
    partial class Main
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(427, 470);
            ForeColor = SystemColors.Highlight;
            Name = "Main";
            Text = "App";
            Load += Main_Load;
            ResumeLayout(false);
        }
    }
}
