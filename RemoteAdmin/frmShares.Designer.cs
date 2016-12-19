namespace RemoteAdmin
{
    partial class frmShares
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrShares1 = new RemoteAdmin.ctrShares();
            this.SuspendLayout();
            // 
            // ctrShares1
            // 
            this.ctrShares1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrShares1.Location = new System.Drawing.Point(0, 0);
            this.ctrShares1.Name = "ctrShares1";
            this.ctrShares1.Size = new System.Drawing.Size(685, 425);
            this.ctrShares1.TabIndex = 0;
            // 
            // frmShares
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 425);
            this.Controls.Add(this.ctrShares1);
            this.Name = "frmShares";
            this.Text = "frmShares";
            this.Load += new System.EventHandler(this.frmShares_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrShares ctrShares1;


    }
}