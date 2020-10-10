using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wjw.editor
{
    public partial class FormNote : Form
    {
        public string Content { get; set; } = string.Empty;
        public FormNote(string preRemark)
        {
            InitializeComponent();
            rtContent.Text = preRemark;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
          
            if (keyData == Keys.Escape)
            {
                this.Content = rtContent.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
                return true;
            }
          

            return base.ProcessCmdKey(ref msg, keyData);
        }

        



    }
}
