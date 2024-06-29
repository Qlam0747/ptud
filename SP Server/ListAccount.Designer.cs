namespace SP_Server
{
    partial class ListAccount
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
            lbxUsers = new ListBox();
            SuspendLayout();
            // 
            // lbxUsers
            // 
            lbxUsers.Dock = DockStyle.Fill;
            lbxUsers.FormattingEnabled = true;
            lbxUsers.ItemHeight = 15;
            lbxUsers.Location = new Point(5, 5);
            lbxUsers.Name = "lbxUsers";
            lbxUsers.ScrollAlwaysVisible = true;
            lbxUsers.Size = new Size(391, 405);
            lbxUsers.TabIndex = 0;
            // 
            // ListAccount
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(401, 415);
            Controls.Add(lbxUsers);
            Name = "ListAccount";
            Padding = new Padding(5);
            Text = "Danh sách các tài khoản";
            Load += ListAccount_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListBox lbxUsers;
    }
}