namespace MiniTool
{
    partial class Make
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.SourceText = new System.Windows.Forms.TextBox();
            this.TargetText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.SourceText);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TargetText);
            this.splitContainer1.Size = new System.Drawing.Size(1153, 792);
            this.splitContainer1.SplitterDistance = 410;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1036, 200);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SourceText
            // 
            this.SourceText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourceText.Location = new System.Drawing.Point(0, 0);
            this.SourceText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SourceText.Multiline = true;
            this.SourceText.Name = "SourceText";
            this.SourceText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SourceText.Size = new System.Drawing.Size(1153, 410);
            this.SourceText.TabIndex = 0;
            // 
            // TargetText
            // 
            this.TargetText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TargetText.Location = new System.Drawing.Point(0, 0);
            this.TargetText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TargetText.Multiline = true;
            this.TargetText.Name = "TargetText";
            this.TargetText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TargetText.Size = new System.Drawing.Size(1153, 377);
            this.TargetText.TabIndex = 0;
            // 
            // Make
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 792);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Make";
            this.Text = "Make";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox SourceText;
        private System.Windows.Forms.TextBox TargetText;
    }
}