namespace LibraryApp
{
    partial class lendingFictionBook
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxInvNum = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonLendFictionBook = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Введите инвентарный номер:";
            // 
            // textBoxInvNum
            // 
            this.textBoxInvNum.Location = new System.Drawing.Point(365, 74);
            this.textBoxInvNum.Name = "textBoxInvNum";
            this.textBoxInvNum.Size = new System.Drawing.Size(233, 20);
            this.textBoxInvNum.TabIndex = 4;
            this.textBoxInvNum.TextChanged += new System.EventHandler(this.textBoxInvNum_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(80, 134);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(643, 150);
            this.dataGridView1.TabIndex = 5;
            // 
            // buttonLendFictionBook
            // 
            this.buttonLendFictionBook.Location = new System.Drawing.Point(619, 338);
            this.buttonLendFictionBook.Name = "buttonLendFictionBook";
            this.buttonLendFictionBook.Size = new System.Drawing.Size(143, 23);
            this.buttonLendFictionBook.TabIndex = 6;
            this.buttonLendFictionBook.Text = "Выдать ученику книгу";
            this.buttonLendFictionBook.UseVisualStyleBackColor = true;
            this.buttonLendFictionBook.Click += new System.EventHandler(this.buttonLendFictionBook_Click);
            // 
            // lendingFictionBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonLendFictionBook);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBoxInvNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "lendingFictionBook";
            this.Text = "Выдать книгу";
            this.Load += new System.EventHandler(this.lendingBooks3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxInvNum;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonLendFictionBook;
    }
}