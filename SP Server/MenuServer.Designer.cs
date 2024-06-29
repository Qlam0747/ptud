namespace SP_Server
{
    partial class MenuServer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "Room1", "Tester", "Admin" }, -1);
            ListViewItem listViewItem2 = new ListViewItem("Room2");
            pClients = new Panel();
            btnBan = new Button();
            btnAllClients = new Button();
            lbxClients = new ListBox();
            lbClients = new Label();
            pHost = new Panel();
            textBox2 = new TextBox();
            tbxIP = new TextBox();
            btnStartServer = new Button();
            lbHost = new Label();
            pRoom = new Panel();
            lvRooms = new ListView();
            ilRooms = new ImageList(components);
            lbRooms = new Label();
            pClients.SuspendLayout();
            pHost.SuspendLayout();
            pRoom.SuspendLayout();
            SuspendLayout();
            // 
            // pClients
            // 
            pClients.Controls.Add(btnBan);
            pClients.Controls.Add(btnAllClients);
            pClients.Controls.Add(lbxClients);
            pClients.Controls.Add(lbClients);
            pClients.Controls.Add(pHost);
            pClients.Dock = DockStyle.Left;
            pClients.Location = new Point(0, 0);
            pClients.Name = "pClients";
            pClients.Padding = new Padding(5);
            pClients.Size = new Size(268, 450);
            pClients.TabIndex = 0;
            // 
            // btnBan
            // 
            btnBan.Dock = DockStyle.Top;
            btnBan.Location = new Point(5, 256);
            btnBan.Name = "btnBan";
            btnBan.Size = new Size(258, 23);
            btnBan.TabIndex = 12;
            btnBan.Text = "Cấm client (chưa phát triển)";
            btnBan.UseVisualStyleBackColor = true;
            // 
            // btnAllClients
            // 
            btnAllClients.Dock = DockStyle.Top;
            btnAllClients.Location = new Point(5, 233);
            btnAllClients.Name = "btnAllClients";
            btnAllClients.Size = new Size(258, 23);
            btnAllClients.TabIndex = 11;
            btnAllClients.Text = "Danh sách toàn bộ tài khoản.";
            btnAllClients.UseVisualStyleBackColor = true;
            btnAllClients.Click += btnAllClients_Click;
            // 
            // lbxClients
            // 
            lbxClients.Dock = DockStyle.Top;
            lbxClients.FormattingEnabled = true;
            lbxClients.ItemHeight = 15;
            lbxClients.Items.AddRange(new object[] { "Client ID: 0 (192.168.1.100)" });
            lbxClients.Location = new Point(5, 79);
            lbxClients.Name = "lbxClients";
            lbxClients.ScrollAlwaysVisible = true;
            lbxClients.Size = new Size(258, 154);
            lbxClients.TabIndex = 10;
            // 
            // lbClients
            // 
            lbClients.AutoSize = true;
            lbClients.Dock = DockStyle.Top;
            lbClients.Location = new Point(5, 64);
            lbClients.Name = "lbClients";
            lbClients.Size = new Size(117, 15);
            lbClients.TabIndex = 9;
            lbClients.Text = "Số lượng đăng nhập:";
            // 
            // pHost
            // 
            pHost.Controls.Add(textBox2);
            pHost.Controls.Add(tbxIP);
            pHost.Controls.Add(btnStartServer);
            pHost.Controls.Add(lbHost);
            pHost.Dock = DockStyle.Top;
            pHost.Location = new Point(5, 5);
            pHost.Name = "pHost";
            pHost.Padding = new Padding(5);
            pHost.Size = new Size(258, 59);
            pHost.TabIndex = 7;
            pHost.Visible = false;
            // 
            // textBox2
            // 
            textBox2.Dock = DockStyle.Right;
            textBox2.Location = new Point(174, 5);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(79, 23);
            textBox2.TabIndex = 11;
            // 
            // tbxIP
            // 
            tbxIP.Dock = DockStyle.Left;
            tbxIP.Location = new Point(5, 5);
            tbxIP.Name = "tbxIP";
            tbxIP.Size = new Size(147, 23);
            tbxIP.TabIndex = 10;
            // 
            // btnStartServer
            // 
            btnStartServer.Dock = DockStyle.Bottom;
            btnStartServer.Location = new Point(5, 31);
            btnStartServer.Name = "btnStartServer";
            btnStartServer.Size = new Size(248, 23);
            btnStartServer.TabIndex = 9;
            btnStartServer.Text = "Chạy Server";
            btnStartServer.UseVisualStyleBackColor = true;
            // 
            // lbHost
            // 
            lbHost.AutoSize = true;
            lbHost.Location = new Point(158, 8);
            lbHost.Name = "lbHost";
            lbHost.Size = new Size(10, 15);
            lbHost.TabIndex = 7;
            lbHost.Text = ":";
            // 
            // pRoom
            // 
            pRoom.Controls.Add(lvRooms);
            pRoom.Controls.Add(lbRooms);
            pRoom.Dock = DockStyle.Left;
            pRoom.Location = new Point(268, 0);
            pRoom.Name = "pRoom";
            pRoom.Padding = new Padding(5);
            pRoom.Size = new Size(378, 450);
            pRoom.TabIndex = 1;
            // 
            // lvRooms
            // 
            lvRooms.Dock = DockStyle.Fill;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            lvRooms.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2 });
            lvRooms.LargeImageList = ilRooms;
            lvRooms.Location = new Point(5, 20);
            lvRooms.Name = "lvRooms";
            lvRooms.ShowItemToolTips = true;
            lvRooms.Size = new Size(368, 425);
            lvRooms.TabIndex = 3;
            lvRooms.UseCompatibleStateImageBehavior = false;
            // 
            // ilRooms
            // 
            ilRooms.ColorDepth = ColorDepth.Depth32Bit;
            ilRooms.ImageSize = new Size(32, 32);
            ilRooms.TransparentColor = Color.Transparent;
            // 
            // lbRooms
            // 
            lbRooms.AutoSize = true;
            lbRooms.Dock = DockStyle.Top;
            lbRooms.Location = new Point(5, 5);
            lbRooms.Name = "lbRooms";
            lbRooms.Size = new Size(95, 15);
            lbRooms.TabIndex = 2;
            lbRooms.Text = "Số lượng phòng:";
            // 
            // MenuServer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pRoom);
            Controls.Add(pClients);
            KeyPreview = true;
            Name = "MenuServer";
            ShowIcon = false;
            Text = "Sharing Place Server";
            Load += MenuServer_Load;
            pClients.ResumeLayout(false);
            pClients.PerformLayout();
            pHost.ResumeLayout(false);
            pHost.PerformLayout();
            pRoom.ResumeLayout(false);
            pRoom.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pClients;
        private Panel pRoom;
        private ListView lvRooms;
        private Label lbRooms;
        private ImageList ilRooms;
        private Panel pHost;
        private Label lbHost;
        private Button btnBan;
        private Button btnAllClients;
        private ListBox lbxClients;
        private Label lbClients;
        private TextBox textBox2;
        private TextBox tbxIP;
        private Button btnStartServer;
    }
}
