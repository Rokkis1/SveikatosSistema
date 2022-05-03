namespace SveikatosSistema
{
    partial class Meniu
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnUpdateUser = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnHealthchanges = new System.Windows.Forms.Button();
            this.btnPhysicalevaluation = new System.Windows.Forms.Button();
            this.btnPsychologicalevaluation = new System.Windows.Forms.Button();
            this.btnReadImage = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDeleteallrecords = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnLogOut, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnExit, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btnUpdateUser, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnReport, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnHealthchanges, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnPhysicalevaluation, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnPsychologicalevaluation, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnReadImage, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.btnDeleteallrecords, 0, 6);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(394, 224);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnUpdateUser
            // 
            this.btnUpdateUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateUser.Location = new System.Drawing.Point(3, 3);
            this.btnUpdateUser.Name = "btnUpdateUser";
            this.btnUpdateUser.Size = new System.Drawing.Size(388, 26);
            this.btnUpdateUser.TabIndex = 0;
            this.btnUpdateUser.Text = "Atnaujinti paskyros duomenis";
            this.btnUpdateUser.UseVisualStyleBackColor = true;
            this.btnUpdateUser.Click += new System.EventHandler(this.btnUpdateUser_Click);
            // 
            // btnReport
            // 
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReport.Location = new System.Drawing.Point(3, 35);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(388, 26);
            this.btnReport.TabIndex = 1;
            this.btnReport.Text = "Pateikti skundą/pastabą";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnHealthchanges
            // 
            this.btnHealthchanges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHealthchanges.Location = new System.Drawing.Point(3, 67);
            this.btnHealthchanges.Name = "btnHealthchanges";
            this.btnHealthchanges.Size = new System.Drawing.Size(388, 26);
            this.btnHealthchanges.TabIndex = 2;
            this.btnHealthchanges.Text = "Pažymėti sveikatos pokyčius";
            this.btnHealthchanges.UseVisualStyleBackColor = true;
            this.btnHealthchanges.Click += new System.EventHandler(this.btnHealthchanges_Click);
            // 
            // btnPhysicalevaluation
            // 
            this.btnPhysicalevaluation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPhysicalevaluation.Location = new System.Drawing.Point(3, 99);
            this.btnPhysicalevaluation.Name = "btnPhysicalevaluation";
            this.btnPhysicalevaluation.Size = new System.Drawing.Size(388, 26);
            this.btnPhysicalevaluation.TabIndex = 3;
            this.btnPhysicalevaluation.Text = "Pažymėti fizinę sveikatą";
            this.btnPhysicalevaluation.UseVisualStyleBackColor = true;
            this.btnPhysicalevaluation.Click += new System.EventHandler(this.btnPhysicalevaluation_Click);
            // 
            // btnPsychologicalevaluation
            // 
            this.btnPsychologicalevaluation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPsychologicalevaluation.Location = new System.Drawing.Point(3, 131);
            this.btnPsychologicalevaluation.Name = "btnPsychologicalevaluation";
            this.btnPsychologicalevaluation.Size = new System.Drawing.Size(388, 26);
            this.btnPsychologicalevaluation.TabIndex = 4;
            this.btnPsychologicalevaluation.Text = "Pažymėti psichinę sveikatą";
            this.btnPsychologicalevaluation.UseVisualStyleBackColor = true;
            this.btnPsychologicalevaluation.Click += new System.EventHandler(this.btnPsychologicalevaluation_Click);
            // 
            // btnReadImage
            // 
            this.btnReadImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadImage.Location = new System.Drawing.Point(3, 163);
            this.btnReadImage.Name = "btnReadImage";
            this.btnReadImage.Size = new System.Drawing.Size(388, 26);
            this.btnReadImage.TabIndex = 5;
            this.btnReadImage.Text = "Nuskaityti nuotrauką";
            this.btnReadImage.UseVisualStyleBackColor = true;
            this.btnReadImage.Click += new System.EventHandler(this.btnReadImage_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.LightGray;
            this.textBox1.Location = new System.Drawing.Point(624, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(173, 20);
            this.textBox1.TabIndex = 2;
            // 
            // btnLogOut
            // 
            this.btnLogOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogOut.Location = new System.Drawing.Point(722, 424);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(75, 23);
            this.btnLogOut.TabIndex = 3;
            this.btnLogOut.Text = "Atsijungti";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Location = new System.Drawing.Point(3, 424);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Išjungti";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDeleteallrecords
            // 
            this.btnDeleteallrecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteallrecords.Location = new System.Drawing.Point(3, 195);
            this.btnDeleteallrecords.Name = "btnDeleteallrecords";
            this.btnDeleteallrecords.Size = new System.Drawing.Size(388, 26);
            this.btnDeleteallrecords.TabIndex = 6;
            this.btnDeleteallrecords.Text = "Pašalinti visus įrašus susijusius su vartotoju";
            this.btnDeleteallrecords.UseVisualStyleBackColor = true;
            this.btnDeleteallrecords.Click += new System.EventHandler(this.btnDeleteallrecords_Click);
            // 
            // Meniu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Meniu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Meniu";
            this.Load += new System.EventHandler(this.Meniu_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnUpdateUser;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnHealthchanges;
        private System.Windows.Forms.Button btnPhysicalevaluation;
        private System.Windows.Forms.Button btnPsychologicalevaluation;
        private System.Windows.Forms.Button btnReadImage;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDeleteallrecords;
    }
}