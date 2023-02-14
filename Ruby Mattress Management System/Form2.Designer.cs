namespace Ruby_Mattress_Management_System
{
    partial class Form2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.jobCardDJV = new System.Windows.Forms.DataGridView();
            this.newButt = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.jobCardDJV)).BeginInit();
            this.SuspendLayout();
            // 
            // jobCardDJV
            // 
            this.jobCardDJV.AllowUserToAddRows = false;
            this.jobCardDJV.AllowUserToDeleteRows = false;
            this.jobCardDJV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.jobCardDJV.DefaultCellStyle = dataGridViewCellStyle2;
            this.jobCardDJV.Location = new System.Drawing.Point(12, 41);
            this.jobCardDJV.Name = "jobCardDJV";
            this.jobCardDJV.ReadOnly = true;
            this.jobCardDJV.Size = new System.Drawing.Size(1137, 465);
            this.jobCardDJV.TabIndex = 0;
            // 
            // newButt
            // 
            this.newButt.Location = new System.Drawing.Point(13, 513);
            this.newButt.Name = "newButt";
            this.newButt.Size = new System.Drawing.Size(75, 23);
            this.newButt.TabIndex = 1;
            this.newButt.Text = "Add New";
            this.newButt.UseVisualStyleBackColor = true;
            this.newButt.Click += new System.EventHandler(this.newButt_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(89, 513);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Show Drawing";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Job Cards List";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 541);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.newButt);
            this.Controls.Add(this.jobCardDJV);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.jobCardDJV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView jobCardDJV;
        private System.Windows.Forms.Button newButt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}