namespace wjw.editor
{
    partial class FormNote
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
            this.rtContent = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtContent
            // 
            this.rtContent.BackColor = System.Drawing.Color.CornflowerBlue;
            this.rtContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtContent.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtContent.ForeColor = System.Drawing.Color.Red;
            this.rtContent.Location = new System.Drawing.Point(0, 0);
            this.rtContent.Name = "rtContent";
            this.rtContent.Size = new System.Drawing.Size(495, 155);
            this.rtContent.TabIndex = 0;
            this.rtContent.Text = "";
            // 
            // FormNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 155);
            this.Controls.Add(this.rtContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormNote";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormNote";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtContent;
    }
}