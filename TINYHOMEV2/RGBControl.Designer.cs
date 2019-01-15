namespace TINYHOMEV2
{
    partial class RGBControl
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
            this.remoteControlGroupBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.blueTextBox = new System.Windows.Forms.TextBox();
            this.greenTextBox = new System.Windows.Forms.TextBox();
            this.redTextBox = new System.Windows.Forms.TextBox();
            this.pcColorPanel = new System.Windows.Forms.Panel();
            this.greenSlider = new System.Windows.Forms.TrackBar();
            this.redSlider = new System.Windows.Forms.TrackBar();
            this.blueSlider = new System.Windows.Forms.TrackBar();
            this.blueSliderLabel = new System.Windows.Forms.Label();
            this.redSliderLabel = new System.Windows.Forms.Label();
            this.greenSliderLabel = new System.Windows.Forms.Label();
            this.remoteControlGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.greenSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.redSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // remoteControlGroupBox
            // 
            this.remoteControlGroupBox.Controls.Add(this.button1);
            this.remoteControlGroupBox.Controls.Add(this.blueTextBox);
            this.remoteControlGroupBox.Controls.Add(this.greenTextBox);
            this.remoteControlGroupBox.Controls.Add(this.redTextBox);
            this.remoteControlGroupBox.Controls.Add(this.pcColorPanel);
            this.remoteControlGroupBox.Controls.Add(this.greenSlider);
            this.remoteControlGroupBox.Controls.Add(this.redSlider);
            this.remoteControlGroupBox.Controls.Add(this.blueSlider);
            this.remoteControlGroupBox.Controls.Add(this.blueSliderLabel);
            this.remoteControlGroupBox.Controls.Add(this.redSliderLabel);
            this.remoteControlGroupBox.Controls.Add(this.greenSliderLabel);
            this.remoteControlGroupBox.Location = new System.Drawing.Point(83, 62);
            this.remoteControlGroupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.remoteControlGroupBox.Name = "remoteControlGroupBox";
            this.remoteControlGroupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.remoteControlGroupBox.Size = new System.Drawing.Size(560, 672);
            this.remoteControlGroupBox.TabIndex = 5;
            this.remoteControlGroupBox.TabStop = false;
            this.remoteControlGroupBox.Text = "Control the color from the PC";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Open Sans", 10F);
            this.button1.ForeColor = System.Drawing.Color.DimGray;
            this.button1.Location = new System.Drawing.Point(288, 588);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(252, 57);
            this.button1.TabIndex = 5;
            this.button1.Text = "CLOSE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // blueTextBox
            // 
            this.blueTextBox.Enabled = false;
            this.blueTextBox.Location = new System.Drawing.Point(195, 614);
            this.blueTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.blueTextBox.Name = "blueTextBox";
            this.blueTextBox.Size = new System.Drawing.Size(58, 31);
            this.blueTextBox.TabIndex = 4;
            // 
            // greenTextBox
            // 
            this.greenTextBox.Enabled = false;
            this.greenTextBox.Location = new System.Drawing.Point(102, 614);
            this.greenTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.greenTextBox.Name = "greenTextBox";
            this.greenTextBox.Size = new System.Drawing.Size(58, 31);
            this.greenTextBox.TabIndex = 4;
            // 
            // redTextBox
            // 
            this.redTextBox.Enabled = false;
            this.redTextBox.Location = new System.Drawing.Point(9, 616);
            this.redTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.redTextBox.Name = "redTextBox";
            this.redTextBox.Size = new System.Drawing.Size(58, 31);
            this.redTextBox.TabIndex = 4;
            // 
            // pcColorPanel
            // 
            this.pcColorPanel.BackColor = System.Drawing.Color.Black;
            this.pcColorPanel.Location = new System.Drawing.Point(288, 394);
            this.pcColorPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pcColorPanel.Name = "pcColorPanel";
            this.pcColorPanel.Size = new System.Drawing.Size(252, 172);
            this.pcColorPanel.TabIndex = 3;
            // 
            // greenSlider
            // 
            this.greenSlider.Location = new System.Drawing.Point(102, 72);
            this.greenSlider.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.greenSlider.Maximum = 255;
            this.greenSlider.Name = "greenSlider";
            this.greenSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.greenSlider.Size = new System.Drawing.Size(90, 506);
            this.greenSlider.TabIndex = 0;
            this.greenSlider.TickFrequency = 5;
            this.greenSlider.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.greenSlider.Scroll += new System.EventHandler(this.greenSlider_Scroll);
            // 
            // redSlider
            // 
            this.redSlider.Location = new System.Drawing.Point(9, 72);
            this.redSlider.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.redSlider.Maximum = 255;
            this.redSlider.Name = "redSlider";
            this.redSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.redSlider.Size = new System.Drawing.Size(90, 506);
            this.redSlider.TabIndex = 0;
            this.redSlider.TickFrequency = 5;
            this.redSlider.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.redSlider.Scroll += new System.EventHandler(this.redSlider_Scroll);
            // 
            // blueSlider
            // 
            this.blueSlider.Location = new System.Drawing.Point(195, 72);
            this.blueSlider.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.blueSlider.Maximum = 255;
            this.blueSlider.Name = "blueSlider";
            this.blueSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.blueSlider.Size = new System.Drawing.Size(90, 506);
            this.blueSlider.TabIndex = 0;
            this.blueSlider.TickFrequency = 5;
            this.blueSlider.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.blueSlider.Scroll += new System.EventHandler(this.blueSlider_Scroll);
            // 
            // blueSliderLabel
            // 
            this.blueSliderLabel.AutoSize = true;
            this.blueSliderLabel.Location = new System.Drawing.Point(204, 583);
            this.blueSliderLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.blueSliderLabel.Name = "blueSliderLabel";
            this.blueSliderLabel.Size = new System.Drawing.Size(55, 25);
            this.blueSliderLabel.TabIndex = 1;
            this.blueSliderLabel.Text = "Blue";
            // 
            // redSliderLabel
            // 
            this.redSliderLabel.AutoSize = true;
            this.redSliderLabel.Location = new System.Drawing.Point(18, 583);
            this.redSliderLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.redSliderLabel.Name = "redSliderLabel";
            this.redSliderLabel.Size = new System.Drawing.Size(51, 25);
            this.redSliderLabel.TabIndex = 1;
            this.redSliderLabel.Text = "Red";
            // 
            // greenSliderLabel
            // 
            this.greenSliderLabel.AutoSize = true;
            this.greenSliderLabel.Location = new System.Drawing.Point(98, 583);
            this.greenSliderLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.greenSliderLabel.Name = "greenSliderLabel";
            this.greenSliderLabel.Size = new System.Drawing.Size(71, 25);
            this.greenSliderLabel.TabIndex = 1;
            this.greenSliderLabel.Text = "Green";
            // 
            // RGBControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(776, 780);
            this.Controls.Add(this.remoteControlGroupBox);
            this.ForeColor = System.Drawing.Color.Snow;
            this.Name = "RGBControl";
            this.Text = "RGBControl";
            this.remoteControlGroupBox.ResumeLayout(false);
            this.remoteControlGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.greenSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.redSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueSlider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox remoteControlGroupBox;
        private System.Windows.Forms.TextBox blueTextBox;
        private System.Windows.Forms.TextBox greenTextBox;
        private System.Windows.Forms.TextBox redTextBox;
        private System.Windows.Forms.Panel pcColorPanel;
        private System.Windows.Forms.TrackBar greenSlider;
        private System.Windows.Forms.TrackBar redSlider;
        private System.Windows.Forms.TrackBar blueSlider;
        private System.Windows.Forms.Label blueSliderLabel;
        private System.Windows.Forms.Label redSliderLabel;
        private System.Windows.Forms.Label greenSliderLabel;
        private System.Windows.Forms.Button button1;
    }
}