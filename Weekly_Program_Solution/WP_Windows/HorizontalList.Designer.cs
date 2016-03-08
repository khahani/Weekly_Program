namespace WP_Windows
{
    partial class HorizontalList
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
            this.FirstItem = new System.Windows.Forms.Label();
            this.SecondItem = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FirstItem
            // 
            this.FirstItem.BackColor = System.Drawing.SystemColors.Info;
            this.FirstItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FirstItem.Location = new System.Drawing.Point(-4, 0);
            this.FirstItem.Name = "FirstItem";
            this.FirstItem.Size = new System.Drawing.Size(113, 25);
            this.FirstItem.TabIndex = 2;
            this.FirstItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SecondItem
            // 
            this.SecondItem.BackColor = System.Drawing.SystemColors.Info;
            this.SecondItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.SecondItem.Location = new System.Drawing.Point(-4, 25);
            this.SecondItem.Name = "SecondItem";
            this.SecondItem.Size = new System.Drawing.Size(113, 30);
            this.SecondItem.TabIndex = 3;
            this.SecondItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HorizontalList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SecondItem);
            this.Controls.Add(this.FirstItem);
            this.Name = "HorizontalList";
            this.Size = new System.Drawing.Size(108, 55);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label FirstItem;
        private System.Windows.Forms.Label SecondItem;
    }
}
