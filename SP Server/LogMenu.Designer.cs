namespace SP_Server
{
    partial class LogMenu
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
            rtbxLog = new RichTextBox();
            SuspendLayout();
            // 
            // rtbxLog
            // 
            rtbxLog.Dock = DockStyle.Fill;
            rtbxLog.EnableAutoDragDrop = true;
            rtbxLog.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbxLog.Location = new Point(5, 5);
            rtbxLog.Name = "rtbxLog";
            rtbxLog.ReadOnly = true;
            rtbxLog.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            rtbxLog.Size = new Size(520, 367);
            rtbxLog.TabIndex = 0;
            rtbxLog.Text = "";
            // 
            // LogMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(530, 377);
            Controls.Add(rtbxLog);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Name = "LogMenu";
            Padding = new Padding(5);
            ShowIcon = false;
            Text = "Lịch sử lệnh";
            Load += Log_Load;
            ResumeLayout(false);
        }

        #endregion

        public static RichTextBox rtbxLog;
    }
}