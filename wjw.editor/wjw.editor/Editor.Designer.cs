namespace wjw.editor
{
    partial class Editor
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
            this.scintillaCode = new ScintillaNET.Scintilla();
            this.pnBookmark = new System.Windows.Forms.Panel();
            this.lvMain = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtSearchBookMark = new System.Windows.Forms.TextBox();
            this.pnSearch = new System.Windows.Forms.Panel();
            this.txtSearchContent = new System.Windows.Forms.TextBox();
            this.lbSearchHistory = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbMatchCase = new System.Windows.Forms.CheckBox();
            this.cbWholeWord = new System.Windows.Forms.CheckBox();
            this.cbRegular = new System.Windows.Forms.CheckBox();
            this.pnSearchTitle = new wjw.editor.DragablePanel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnBookmarkTitle = new wjw.editor.DragablePanel();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnSerachBegin = new System.Windows.Forms.Button();
            this.btnSearchUp = new System.Windows.Forms.Button();
            this.btnSearchDown = new System.Windows.Forms.Button();
            this.btnViewText = new System.Windows.Forms.Button();
            this.btnDeleAllbookMark = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnBookmark.SuspendLayout();
            this.pnSearch.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnSearchTitle.SuspendLayout();
            this.pnBookmarkTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // scintillaCode
            // 
            this.scintillaCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.scintillaCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaCode.Location = new System.Drawing.Point(0, 0);
            this.scintillaCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.scintillaCode.Name = "scintillaCode";
            this.scintillaCode.Size = new System.Drawing.Size(1427, 485);
            this.scintillaCode.TabIndex = 3;
            this.scintillaCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scintillaCode_KeyPress);
            // 
            // pnBookmark
            // 
            this.pnBookmark.BackColor = System.Drawing.Color.White;
            this.pnBookmark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnBookmark.Controls.Add(this.lvMain);
            this.pnBookmark.Controls.Add(this.txtSearchBookMark);
            this.pnBookmark.Controls.Add(this.pnBookmarkTitle);
            this.pnBookmark.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBookmark.Location = new System.Drawing.Point(0, 485);
            this.pnBookmark.Name = "pnBookmark";
            this.pnBookmark.Size = new System.Drawing.Size(1427, 300);
            this.pnBookmark.TabIndex = 12;
            this.pnBookmark.Visible = false;
            this.pnBookmark.Resize += new System.EventHandler(this.pnBookmark_Resize);
            // 
            // lvMain
            // 
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMain.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMain.HideSelection = false;
            this.lvMain.Location = new System.Drawing.Point(0, 27);
            this.lvMain.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(1425, 248);
            this.lvMain.TabIndex = 3;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvMain_KeyUp);
            this.lvMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseDoubleClick);
            this.lvMain.Resize += new System.EventHandler(this.lvMain_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Line";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Text";
            this.columnHeader2.Width = 900;
            // 
            // txtSearchBookMark
            // 
            this.txtSearchBookMark.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtSearchBookMark.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchBookMark.Location = new System.Drawing.Point(0, 275);
            this.txtSearchBookMark.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtSearchBookMark.Name = "txtSearchBookMark";
            this.txtSearchBookMark.Size = new System.Drawing.Size(1425, 23);
            this.txtSearchBookMark.TabIndex = 2;
            this.txtSearchBookMark.TextChanged += new System.EventHandler(this.txtSearch_TextChanged_1);
            this.txtSearchBookMark.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // pnSearch
            // 
            this.pnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnSearch.BackColor = System.Drawing.Color.White;
            this.pnSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnSearch.Controls.Add(this.txtSearchContent);
            this.pnSearch.Controls.Add(this.lbSearchHistory);
            this.pnSearch.Controls.Add(this.panel2);
            this.pnSearch.Controls.Add(this.pnSearchTitle);
            this.pnSearch.Controls.Add(this.panel3);
            this.pnSearch.Location = new System.Drawing.Point(266, 88);
            this.pnSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnSearch.Name = "pnSearch";
            this.pnSearch.Size = new System.Drawing.Size(733, 258);
            this.pnSearch.TabIndex = 11;
            this.pnSearch.Visible = false;
            // 
            // txtSearchContent
            // 
            this.txtSearchContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchContent.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchContent.Location = new System.Drawing.Point(0, 57);
            this.txtSearchContent.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtSearchContent.Multiline = true;
            this.txtSearchContent.Name = "txtSearchContent";
            this.txtSearchContent.Size = new System.Drawing.Size(425, 169);
            this.txtSearchContent.TabIndex = 6;
            this.txtSearchContent.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.txtSearchContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            this.txtSearchContent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSearch_KeyPress);
            // 
            // lbSearchHistory
            // 
            this.lbSearchHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbSearchHistory.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbSearchHistory.FormattingEnabled = true;
            this.lbSearchHistory.ItemHeight = 15;
            this.lbSearchHistory.Location = new System.Drawing.Point(425, 57);
            this.lbSearchHistory.Name = "lbSearchHistory";
            this.lbSearchHistory.Size = new System.Drawing.Size(306, 169);
            this.lbSearchHistory.TabIndex = 20;
            this.lbSearchHistory.Visible = false;
            this.lbSearchHistory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbSearchHistory_MouseDoubleClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btnHistory);
            this.panel2.Controls.Add(this.btnSerachBegin);
            this.panel2.Controls.Add(this.btnSearchUp);
            this.panel2.Controls.Add(this.btnSearchDown);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(731, 30);
            this.panel2.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.cbMatchCase);
            this.panel3.Controls.Add(this.cbWholeWord);
            this.panel3.Controls.Add(this.cbRegular);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(0, 226);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(731, 30);
            this.panel3.TabIndex = 15;
            // 
            // cbMatchCase
            // 
            this.cbMatchCase.AutoSize = true;
            this.cbMatchCase.Location = new System.Drawing.Point(7, 6);
            this.cbMatchCase.Name = "cbMatchCase";
            this.cbMatchCase.Size = new System.Drawing.Size(96, 18);
            this.cbMatchCase.TabIndex = 11;
            this.cbMatchCase.Text = "Match case";
            this.cbMatchCase.UseVisualStyleBackColor = true;
            this.cbMatchCase.CheckedChanged += new System.EventHandler(this.cbMatchCase_CheckedChanged);
            // 
            // cbWholeWord
            // 
            this.cbWholeWord.AutoSize = true;
            this.cbWholeWord.Location = new System.Drawing.Point(126, 6);
            this.cbWholeWord.Name = "cbWholeWord";
            this.cbWholeWord.Size = new System.Drawing.Size(96, 18);
            this.cbWholeWord.TabIndex = 12;
            this.cbWholeWord.Text = "Whole word";
            this.cbWholeWord.UseVisualStyleBackColor = true;
            this.cbWholeWord.CheckedChanged += new System.EventHandler(this.cbWholeWord_CheckedChanged);
            // 
            // cbRegular
            // 
            this.cbRegular.AutoSize = true;
            this.cbRegular.Location = new System.Drawing.Point(248, 6);
            this.cbRegular.Name = "cbRegular";
            this.cbRegular.Size = new System.Drawing.Size(75, 18);
            this.cbRegular.TabIndex = 13;
            this.cbRegular.Text = "Regular";
            this.cbRegular.UseVisualStyleBackColor = true;
            this.cbRegular.CheckedChanged += new System.EventHandler(this.cbRegular_CheckedChanged);
            // 
            // pnSearchTitle
            // 
            this.pnSearchTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.pnSearchTitle.Controls.Add(this.label1);
            this.pnSearchTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnSearchTitle.Dragable = true;
            this.pnSearchTitle.Location = new System.Drawing.Point(0, 0);
            this.pnSearchTitle.Name = "pnSearchTitle";
            this.pnSearchTitle.Size = new System.Drawing.Size(731, 27);
            this.pnSearchTitle.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "Search";
            // 
            // pnBookmarkTitle
            // 
            this.pnBookmarkTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.pnBookmarkTitle.Controls.Add(this.btnViewText);
            this.pnBookmarkTitle.Controls.Add(this.btnDeleAllbookMark);
            this.pnBookmarkTitle.Controls.Add(this.btnRefresh);
            this.pnBookmarkTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnBookmarkTitle.Dragable = true;
            this.pnBookmarkTitle.Location = new System.Drawing.Point(0, 0);
            this.pnBookmarkTitle.Name = "pnBookmarkTitle";
            this.pnBookmarkTitle.Size = new System.Drawing.Size(1425, 27);
            this.pnBookmarkTitle.TabIndex = 0;
            // 
            // btnHistory
            // 
            this.btnHistory.Image = global::wjw.editor.Properties.Resources.historysmall;
            this.btnHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistory.Location = new System.Drawing.Point(321, 3);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(100, 23);
            this.btnHistory.TabIndex = 11;
            this.btnHistory.Text = "History";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnSerachBegin
            // 
            this.btnSerachBegin.Image = global::wjw.editor.Properties.Resource.begin;
            this.btnSerachBegin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSerachBegin.Location = new System.Drawing.Point(215, 3);
            this.btnSerachBegin.Name = "btnSerachBegin";
            this.btnSerachBegin.Size = new System.Drawing.Size(100, 23);
            this.btnSerachBegin.TabIndex = 10;
            this.btnSerachBegin.Text = "Begin";
            this.btnSerachBegin.UseVisualStyleBackColor = true;
            this.btnSerachBegin.Click += new System.EventHandler(this.btnSerachBegin_Click);
            // 
            // btnSearchUp
            // 
            this.btnSearchUp.Image = global::wjw.editor.Properties.Resource.up;
            this.btnSearchUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchUp.Location = new System.Drawing.Point(109, 3);
            this.btnSearchUp.Name = "btnSearchUp";
            this.btnSearchUp.Size = new System.Drawing.Size(100, 23);
            this.btnSearchUp.TabIndex = 10;
            this.btnSearchUp.Text = "Up";
            this.btnSearchUp.UseVisualStyleBackColor = true;
            this.btnSearchUp.Click += new System.EventHandler(this.btnSearchUp_Click);
            // 
            // btnSearchDown
            // 
            this.btnSearchDown.Image = global::wjw.editor.Properties.Resource.down;
            this.btnSearchDown.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSearchDown.Location = new System.Drawing.Point(3, 3);
            this.btnSearchDown.Name = "btnSearchDown";
            this.btnSearchDown.Size = new System.Drawing.Size(100, 23);
            this.btnSearchDown.TabIndex = 10;
            this.btnSearchDown.Text = "Down";
            this.btnSearchDown.UseVisualStyleBackColor = true;
            this.btnSearchDown.Click += new System.EventHandler(this.btnSearchDown_Click);
            // 
            // btnViewText
            // 
            this.btnViewText.Image = global::wjw.editor.Properties.Resource.text_lines_11_611584327087px_1287268_easyicon_net;
            this.btnViewText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewText.Location = new System.Drawing.Point(215, 2);
            this.btnViewText.Name = "btnViewText";
            this.btnViewText.Size = new System.Drawing.Size(100, 23);
            this.btnViewText.TabIndex = 23;
            this.btnViewText.Tag = "View Text";
            this.btnViewText.Text = "View Text";
            this.btnViewText.UseVisualStyleBackColor = true;
            this.btnViewText.Click += new System.EventHandler(this.btnViewText_Click);
            // 
            // btnDeleAllbookMark
            // 
            this.btnDeleAllbookMark.Image = global::wjw.editor.Properties.Resource.close2;
            this.btnDeleAllbookMark.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleAllbookMark.Location = new System.Drawing.Point(109, 2);
            this.btnDeleAllbookMark.Name = "btnDeleAllbookMark";
            this.btnDeleAllbookMark.Size = new System.Drawing.Size(100, 23);
            this.btnDeleAllbookMark.TabIndex = 22;
            this.btnDeleAllbookMark.Text = "Del All";
            this.btnDeleAllbookMark.UseVisualStyleBackColor = true;
            this.btnDeleAllbookMark.Click += new System.EventHandler(this.btnDeleAllbookMark_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::wjw.editor.Properties.Resource.refresh;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 23);
            this.btnRefresh.TabIndex = 20;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnSearch);
            this.Controls.Add(this.scintillaCode);
            this.Controls.Add(this.pnBookmark);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Editor";
            this.Size = new System.Drawing.Size(1427, 785);
            this.Load += new System.EventHandler(this.Editor_Load);
            this.pnBookmark.ResumeLayout(false);
            this.pnBookmark.PerformLayout();
            this.pnSearch.ResumeLayout(false);
            this.pnSearch.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnSearchTitle.ResumeLayout(false);
            this.pnSearchTitle.PerformLayout();
            this.pnBookmarkTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ScintillaNET.Scintilla scintillaCode;
        private System.Windows.Forms.TextBox txtSearchContent;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cbMatchCase;
        private System.Windows.Forms.Button btnSerachBegin;
        private System.Windows.Forms.CheckBox cbRegular;
        private System.Windows.Forms.Button btnSearchUp;
        private System.Windows.Forms.Button btnSearchDown;
        private System.Windows.Forms.CheckBox cbWholeWord;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
      //  private System.Windows.Forms.Button btnCloseSearch;
        private System.Windows.Forms.Panel pnBookmark;
        private DragablePanel pnBookmarkTitle;
       // private System.Windows.Forms.Button bnCloseBookmark;
        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox txtSearchBookMark;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDeleAllbookMark;
        private DragablePanel pnSearchTitle;
        private System.Windows.Forms.Panel pnSearch;
        private System.Windows.Forms.Button btnViewText;
        private System.Windows.Forms.ListBox lbSearchHistory;
        private System.Windows.Forms.Button btnHistory;
    }
}