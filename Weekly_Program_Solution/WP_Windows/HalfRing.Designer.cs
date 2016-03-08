namespace WP_Windows
{
    partial class HalfRing
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SecondHR = new System.Windows.Forms.Label();
            this.FirstHR = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SecondHR
            // 
            this.SecondHR.BackColor = System.Drawing.SystemColors.Info;
            this.SecondHR.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.SecondHR.Location = new System.Drawing.Point(-4, 0);
            this.SecondHR.Name = "SecondHR";
            this.SecondHR.Size = new System.Drawing.Size(54, 57);
            this.SecondHR.TabIndex = 1;
            this.SecondHR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FirstHR
            // 
            this.FirstHR.BackColor = System.Drawing.SystemColors.Info;
            this.FirstHR.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FirstHR.Location = new System.Drawing.Point(56, 0);
            this.FirstHR.Name = "FirstHR";
            this.FirstHR.Size = new System.Drawing.Size(54, 57);
            this.FirstHR.TabIndex = 2;
            this.FirstHR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HalfRing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FirstHR);
            this.Controls.Add(this.SecondHR);
            this.Name = "HalfRing";
            this.Size = new System.Drawing.Size(112, 58);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label SecondHR;
        private System.Windows.Forms.Label FirstHR;
    }
}
