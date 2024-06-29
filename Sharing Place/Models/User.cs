using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharing_Place.Models
{
    public class User
    {
        string username, fullname, id, email, nickname, birth;
        bool gender;
        public User()
        {
            username = "usertest";
            id = "0";
            fullname = "Admin Tester";
            nickname = "Tester";
            email = "22520702@gm.uit.edu.vn";
            birth = "1/1/2000";
            gender = true;
        }
        public User(string id)
        {
            this.id = id;
            getInfo(id);
        }
        private void getInfo(string id)
        {
            string query = string.Format("SELECT * FROM [User].[Users],[User].[Info] WHERE id = '{0}';", id);
            DataTable dt = SqlQuery.getData(query);
            username = dt.Rows[0]["username"].ToString();
            email = dt.Rows[0]["email"].ToString();
            fullname = dt.Rows[0]["fullname"].ToString();
            nickname = dt.Rows[0]["nickname"].ToString();
            birth = dt.Rows[0]["birth"].ToString();
            gender = !(dt.Rows[0]["gender"].ToString().Contains("female"));
        }
    }
}
