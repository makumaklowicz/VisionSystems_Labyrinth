namespace Projekt_Labirynth
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
            this.Viewport = new System.Windows.Forms.PictureBox();
            this.Load_IMG_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Viewport)).BeginInit();
            this.SuspendLayout();
            // 
            // Viewport
            // 
            this.Viewport.BackColor = System.Drawing.Color.White;
            this.Viewport.Location = new System.Drawing.Point(287, 133);
            this.Viewport.Name = "Viewport";
            this.Viewport.Size = new System.Drawing.Size(596, 474);
            this.Viewport.TabIndex = 0;
            this.Viewport.TabStop = false;
            // 
            // Load_IMG_button
            // 
            this.Load_IMG_button.Location = new System.Drawing.Point(375, 30);
            this.Load_IMG_button.Name = "Load_IMG_button";
            this.Load_IMG_button.Size = new System.Drawing.Size(440, 73);
            this.Load_IMG_button.TabIndex = 1;
            this.Load_IMG_button.Text = "Load Image";
            this.Load_IMG_button.UseVisualStyleBackColor = true;
            this.Load_IMG_button.Click += new System.EventHandler(this.Load_IMG_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 750);
            this.Controls.Add(this.Load_IMG_button);
            this.Controls.Add(this.Viewport);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Viewport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Viewport;
        private System.Windows.Forms.Button Load_IMG_button;
    }
}

