using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Server
{
    public partial class ListAccount : Form
    {
        List<User> Users = new List<User>();
        public ListAccount()
        {
            InitializeComponent();
        }

        private void ListAccount_Load(object sender, EventArgs e)
        {
            lbxUsers.Items.Clear(); 
            Users = loadUsers();
            foreach (User user in Users)
            {
                string data = user.Id + ": " + user.Username + " - " + user.Info.Fullname + " (" + user.Info.Nickname + ")";
                lbxUsers.Items.Add(data);
            }
        }

        //Load dữ liệu tài khoản User khi có id
        public List<User> loadUsers()
        {
            List<User> users = new List<User>();
            string query = "SELECT * FROM [User].[Users], [User].[Info] WHERE id = _id;";
            DataTable dt = SqlQuery.getData(query);
            if (dt.Rows.Count <= 0) { MenuServer.writeLog("Không lấy được dữ liệu User"); return null; }
            for (int i = 0; i < dt.Rows.Count; i++) users.Add(User.loadUser(dt, i));
            return users;
        }
    }
}
