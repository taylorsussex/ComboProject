namespace ComboProject
{
    partial class Form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.browse = new System.Windows.Forms.Button();
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.comboTextBox = new System.Windows.Forms.TextBox();
            this.simulate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.damageTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "CSV source file";
            // 
            // browse
            // 
            this.browse.Location = new System.Drawing.Point(197, 25);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(75, 23);
            this.browse.TabIndex = 9;
            this.browse.Text = "Browse";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // fileTextBox
            // 
            this.fileTextBox.Location = new System.Drawing.Point(12, 28);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.Size = new System.Drawing.Size(179, 20);
            this.fileTextBox.TabIndex = 8;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // comboTextBox
            // 
            this.comboTextBox.Location = new System.Drawing.Point(12, 101);
            this.comboTextBox.Name = "comboTextBox";
            this.comboTextBox.Size = new System.Drawing.Size(179, 20);
            this.comboTextBox.TabIndex = 8;
            // 
            // simulate
            // 
            this.simulate.Enabled = false;
            this.simulate.Location = new System.Drawing.Point(197, 98);
            this.simulate.Name = "simulate";
            this.simulate.Size = new System.Drawing.Size(75, 23);
            this.simulate.TabIndex = 9;
            this.simulate.Text = "Simulate";
            this.simulate.UseVisualStyleBackColor = true;
            this.simulate.Click += new System.EventHandler(this.simulate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Combo";
            // 
            // damageTextBox
            // 
            this.damageTextBox.Location = new System.Drawing.Point(12, 174);
            this.damageTextBox.Name = "damageTextBox";
            this.damageTextBox.Size = new System.Drawing.Size(179, 20);
            this.damageTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Combo Damage";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.simulate);
            this.Controls.Add(this.browse);
            this.Controls.Add(this.damageTextBox);
            this.Controls.Add(this.comboTextBox);
            this.Controls.Add(this.fileTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.TextBox fileTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox comboTextBox;
        private System.Windows.Forms.Button simulate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox damageTextBox;
        private System.Windows.Forms.Label label3;
    }
}

