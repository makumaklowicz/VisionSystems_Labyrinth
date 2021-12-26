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
            this.DrawVectors = new System.Windows.Forms.Button();
            this.DrawSolve = new System.Windows.Forms.Button();
            this.Segmentation = new System.Windows.Forms.Button();
            this.viewport_Path = new System.Windows.Forms.PictureBox();
            this.viewport_Walls = new System.Windows.Forms.PictureBox();
            this.viewport_Dot = new System.Windows.Forms.PictureBox();
            this.command_List = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Viewport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewport_Path)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewport_Walls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewport_Dot)).BeginInit();
            this.SuspendLayout();
            // 
            // Viewport
            // 
            this.Viewport.BackColor = System.Drawing.Color.White;
            this.Viewport.Location = new System.Drawing.Point(29, 118);
            this.Viewport.Name = "Viewport";
            this.Viewport.Size = new System.Drawing.Size(509, 455);
            this.Viewport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Viewport.TabIndex = 0;
            this.Viewport.TabStop = false;
            this.Viewport.Click += new System.EventHandler(this.Viewport_Click);
            // 
            // Load_IMG_button
            // 
            this.Load_IMG_button.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Load_IMG_button.Location = new System.Drawing.Point(64, 32);
            this.Load_IMG_button.Name = "Load_IMG_button";
            this.Load_IMG_button.Size = new System.Drawing.Size(440, 78);
            this.Load_IMG_button.TabIndex = 1;
            this.Load_IMG_button.Text = "Load Image";
            this.Load_IMG_button.UseVisualStyleBackColor = true;
            this.Load_IMG_button.Click += new System.EventHandler(this.Load_IMG_button_Click);
            // 
            // DrawVectors
            // 
            this.DrawVectors.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawVectors.Location = new System.Drawing.Point(29, 684);
            this.DrawVectors.Name = "DrawVectors";
            this.DrawVectors.Size = new System.Drawing.Size(279, 32);
            this.DrawVectors.TabIndex = 2;
            this.DrawVectors.Text = "Draw possible moves";
            this.DrawVectors.UseVisualStyleBackColor = true;
            // 
            // DrawSolve
            // 
            this.DrawSolve.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawSolve.Location = new System.Drawing.Point(29, 757);
            this.DrawSolve.Name = "DrawSolve";
            this.DrawSolve.Size = new System.Drawing.Size(279, 32);
            this.DrawSolve.TabIndex = 3;
            this.DrawSolve.Text = "Draw labyrinth solve";
            this.DrawSolve.UseVisualStyleBackColor = true;
            // 
            // Segmentation
            // 
            this.Segmentation.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Segmentation.Location = new System.Drawing.Point(29, 616);
            this.Segmentation.Name = "Segmentation";
            this.Segmentation.Size = new System.Drawing.Size(279, 32);
            this.Segmentation.TabIndex = 4;
            this.Segmentation.Text = "Draw different views";
            this.Segmentation.UseVisualStyleBackColor = true;
            this.Segmentation.Click += new System.EventHandler(this.Segmentation_Click);
            // 
            // viewport_Path
            // 
            this.viewport_Path.BackColor = System.Drawing.Color.White;
            this.viewport_Path.Location = new System.Drawing.Point(574, 118);
            this.viewport_Path.Name = "viewport_Path";
            this.viewport_Path.Size = new System.Drawing.Size(383, 343);
            this.viewport_Path.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.viewport_Path.TabIndex = 5;
            this.viewport_Path.TabStop = false;
            // 
            // viewport_Walls
            // 
            this.viewport_Walls.BackColor = System.Drawing.Color.White;
            this.viewport_Walls.Location = new System.Drawing.Point(981, 118);
            this.viewport_Walls.Name = "viewport_Walls";
            this.viewport_Walls.Size = new System.Drawing.Size(383, 343);
            this.viewport_Walls.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.viewport_Walls.TabIndex = 6;
            this.viewport_Walls.TabStop = false;
            // 
            // viewport_Dot
            // 
            this.viewport_Dot.BackColor = System.Drawing.Color.White;
            this.viewport_Dot.Location = new System.Drawing.Point(1390, 118);
            this.viewport_Dot.Name = "viewport_Dot";
            this.viewport_Dot.Size = new System.Drawing.Size(383, 343);
            this.viewport_Dot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.viewport_Dot.TabIndex = 7;
            this.viewport_Dot.TabStop = false;
            // 
            // command_List
            // 
            this.command_List.Location = new System.Drawing.Point(1180, 684);
            this.command_List.Name = "command_List";
            this.command_List.Size = new System.Drawing.Size(593, 151);
            this.command_List.TabIndex = 8;
            this.command_List.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(571, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "Path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(977, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 19);
            this.label2.TabIndex = 10;
            this.label2.Text = "Walls:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1386, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "Dot";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1177, 652);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 19);
            this.label4.TabIndex = 12;
            this.label4.Text = "Command list:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1820, 852);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.command_List);
            this.Controls.Add(this.viewport_Dot);
            this.Controls.Add(this.viewport_Walls);
            this.Controls.Add(this.viewport_Path);
            this.Controls.Add(this.Segmentation);
            this.Controls.Add(this.DrawSolve);
            this.Controls.Add(this.DrawVectors);
            this.Controls.Add(this.Load_IMG_button);
            this.Controls.Add(this.Viewport);
            this.Font = new System.Drawing.Font("Maiandra GD", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Labyrinth_Solver";
            ((System.ComponentModel.ISupportInitialize)(this.Viewport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewport_Path)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewport_Walls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewport_Dot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Viewport;
        private System.Windows.Forms.Button Load_IMG_button;
        private System.Windows.Forms.Button DrawVectors;
        private System.Windows.Forms.Button DrawSolve;
        private System.Windows.Forms.Button Segmentation;
        private System.Windows.Forms.PictureBox viewport_Path;
        private System.Windows.Forms.PictureBox viewport_Walls;
        private System.Windows.Forms.PictureBox viewport_Dot;
        private System.Windows.Forms.ListView command_List;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

