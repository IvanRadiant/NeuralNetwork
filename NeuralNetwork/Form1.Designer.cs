namespace NeuralNetwork
{
    partial class MainForm
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
            this.InputPictureGroupBox = new System.Windows.Forms.GroupBox();
            this.InputPictureBox = new System.Windows.Forms.PictureBox();
            this.ImgLoadBtn = new System.Windows.Forms.Button();
            this.GuessBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.mainTextBox = new System.Windows.Forms.TextBox();
            this.trainBtn = new System.Windows.Forms.Button();
            this.InputPictureGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InputPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // InputPictureGroupBox
            // 
            this.InputPictureGroupBox.Controls.Add(this.InputPictureBox);
            this.InputPictureGroupBox.Location = new System.Drawing.Point(12, 12);
            this.InputPictureGroupBox.Name = "InputPictureGroupBox";
            this.InputPictureGroupBox.Size = new System.Drawing.Size(205, 229);
            this.InputPictureGroupBox.TabIndex = 1;
            this.InputPictureGroupBox.TabStop = false;
            this.InputPictureGroupBox.Text = "Изображение";
            // 
            // InputPictureBox
            // 
            this.InputPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputPictureBox.Enabled = false;
            this.InputPictureBox.Location = new System.Drawing.Point(3, 16);
            this.InputPictureBox.Name = "InputPictureBox";
            this.InputPictureBox.Padding = new System.Windows.Forms.Padding(70);
            this.InputPictureBox.Size = new System.Drawing.Size(199, 210);
            this.InputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.InputPictureBox.TabIndex = 0;
            this.InputPictureBox.TabStop = false;
            // 
            // ImgLoadBtn
            // 
            this.ImgLoadBtn.Location = new System.Drawing.Point(12, 245);
            this.ImgLoadBtn.Name = "ImgLoadBtn";
            this.ImgLoadBtn.Size = new System.Drawing.Size(205, 30);
            this.ImgLoadBtn.TabIndex = 2;
            this.ImgLoadBtn.Text = "Загрузить изображение";
            this.ImgLoadBtn.UseVisualStyleBackColor = true;
            this.ImgLoadBtn.Click += new System.EventHandler(this.ImgLoadBtn_Click);
            // 
            // GuessBtn
            // 
            this.GuessBtn.Location = new System.Drawing.Point(12, 281);
            this.GuessBtn.Name = "GuessBtn";
            this.GuessBtn.Size = new System.Drawing.Size(205, 31);
            this.GuessBtn.TabIndex = 3;
            this.GuessBtn.Text = "Определить цифру";
            this.GuessBtn.UseVisualStyleBackColor = true;
            this.GuessBtn.Click += new System.EventHandler(this.GuessBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog";
            // 
            // mainTextBox
            // 
            this.mainTextBox.Location = new System.Drawing.Point(223, 28);
            this.mainTextBox.Multiline = true;
            this.mainTextBox.Name = "mainTextBox";
            this.mainTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mainTextBox.Size = new System.Drawing.Size(307, 210);
            this.mainTextBox.TabIndex = 5;
            // 
            // trainBtn
            // 
            this.trainBtn.Location = new System.Drawing.Point(251, 281);
            this.trainBtn.Name = "trainBtn";
            this.trainBtn.Size = new System.Drawing.Size(75, 23);
            this.trainBtn.TabIndex = 6;
            this.trainBtn.Text = "Train";
            this.trainBtn.UseVisualStyleBackColor = true;
            this.trainBtn.Click += new System.EventHandler(this.trainBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 320);
            this.Controls.Add(this.trainBtn);
            this.Controls.Add(this.mainTextBox);
            this.Controls.Add(this.GuessBtn);
            this.Controls.Add(this.ImgLoadBtn);
            this.Controls.Add(this.InputPictureGroupBox);
            this.Name = "MainForm";
            this.Text = "Neural Network";
            this.InputPictureGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InputPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox InputPictureBox;
        private System.Windows.Forms.GroupBox InputPictureGroupBox;
        private System.Windows.Forms.Button ImgLoadBtn;
        private System.Windows.Forms.Button GuessBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox mainTextBox;
        private System.Windows.Forms.Button trainBtn;
    }
}

