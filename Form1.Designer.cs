namespace _3DGame
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.FPS = new System.Windows.Forms.Label();
            this.Health = new System.Windows.Forms.Label();
            this.Ammo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FPS
            // 
            this.FPS.AutoSize = true;
            this.FPS.Location = new System.Drawing.Point(13, 13);
            this.FPS.Name = "FPS";
            this.FPS.Size = new System.Drawing.Size(34, 17);
            this.FPS.TabIndex = 0;
            this.FPS.Text = "FPS";
            // 
            // Health
            // 
            this.Health.AutoSize = true;
            this.Health.Location = new System.Drawing.Point(294, 13);
            this.Health.Name = "Health";
            this.Health.Size = new System.Drawing.Size(49, 17);
            this.Health.TabIndex = 1;
            this.Health.Text = "Health";
            // 
            // Ammo
            // 
            this.Ammo.AutoSize = true;
            this.Ammo.Location = new System.Drawing.Point(296, 55);
            this.Ammo.Name = "Ammo";
            this.Ammo.Size = new System.Drawing.Size(47, 17);
            this.Ammo.TabIndex = 2;
            this.Ammo.Text = "Ammo";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.Ammo);
            this.Controls.Add(this.Health);
            this.Controls.Add(this.FPS);
            this.Name = "Form1";
            this.Text = "RuVis";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FPS;
        private System.Windows.Forms.Label Health;
        private System.Windows.Forms.Label Ammo;
    }
}

