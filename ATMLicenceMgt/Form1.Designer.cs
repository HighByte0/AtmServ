namespace ATMLicenceMgt
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_GenerateData = new System.Windows.Forms.Button();
            this.txt_BankName = new System.Windows.Forms.TextBox();
            this.txt_AtmNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_GenerateData
            // 
            this.btn_GenerateData.Location = new System.Drawing.Point(130, 123);
            this.btn_GenerateData.Name = "btn_GenerateData";
            this.btn_GenerateData.Size = new System.Drawing.Size(159, 23);
            this.btn_GenerateData.TabIndex = 0;
            this.btn_GenerateData.Text = "Générer données client";
            this.btn_GenerateData.UseVisualStyleBackColor = true;
            this.btn_GenerateData.Click += new System.EventHandler(this.btn_GenerateData_Click);
            // 
            // txt_BankName
            // 
            this.txt_BankName.Location = new System.Drawing.Point(302, 29);
            this.txt_BankName.Name = "txt_BankName";
            this.txt_BankName.Size = new System.Drawing.Size(100, 20);
            this.txt_BankName.TabIndex = 1;
            // 
            // txt_AtmNumber
            // 
            this.txt_AtmNumber.Location = new System.Drawing.Point(302, 77);
            this.txt_AtmNumber.Name = "txt_AtmNumber";
            this.txt_AtmNumber.Size = new System.Drawing.Size(100, 20);
            this.txt_AtmNumber.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Banque";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nombre de GAB";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(217, 104);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 262);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_AtmNumber);
            this.Controls.Add(this.txt_BankName);
            this.Controls.Add(this.btn_GenerateData);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_GenerateData;
        private System.Windows.Forms.TextBox txt_BankName;
        private System.Windows.Forms.TextBox txt_AtmNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblError;
    }
}

