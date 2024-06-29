using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SP_Server
{
    public class UserClient
    {
        public string Id;
        public IPEndPoint IPaddress;
        public UserClient(string id, IPEndPoint ip)
        {
            this.Id = id;
            this.IPaddress = ip;
        }
    }
    public class User
    {
        public string Id;
        public string Username;
        public string Email;
        public UserInfo Info;
        public User(string id, string username, string email, UserInfo info)
        {
            this.Id = id;
            this.Username = username;
            this.Email = email;
            this.Info = info;
        }

        //Load dữ liệu tài khoản User khi có DataTable
        public static User loadUser(DataTable dt, int index = 0)
        {
            string id = dt.Rows[index]["id"].ToString();
            string username = dt.Rows[index]["username"].ToString();
            string email = dt.Rows[index]["email"].ToString();
            UserInfo ui = UserInfo.loadUserInfo(dt, index);
            return new User(id, username, email, ui);
        }
    }
    public class UserInfo
    {
        public string Id;
        public string Fullname;
        public string Nickname;
        public string Gender;
        public string Avatar;
        public UserInfo(string id, string fullname, string nickname, string gender, string avatar="")
        {
            this.Id = id;
            this.Fullname = fullname;
            this.Nickname = nickname;
            this.Gender = gender;
            this.Avatar = avatar;
        }

        //Load thông tin User khi có DataTable
        public static UserInfo loadUserInfo(DataTable dt, int index = 0)
        {
            string id = dt.Rows[index]["_id"].ToString();
            string fname = dt.Rows[index]["fullname"].ToString();
            string nname = dt.Rows[index]["nickname"].ToString();
            string gender = dt.Rows[index]["gender"].ToString();
            UserInfo ui = new UserInfo(id, fname, nname, gender);
            return ui;
        }
    }
    public class Room
    {
        public string Id;
        public string Name;
        public string Description;
        public string Type; //private | public
        public string Owner;
        public List<Post> Posts;
        public List<UserInfo> Members;
        public Room(string id, string name, string type, string owner)
        {
            this.Id = id;
            this.Name = name;
            this.Owner = owner;
            this.Type = type;
            this.Posts = new List<Post>();
            this.Members = new List<UserInfo>();
        }
        public bool isPrivate()
        {
            return this.Type.Contains("private");
        }

    }

    public class Post
    {
        public string Id;
        public string Title;
        public string Body;
        public string Status;
        public string Owner;
        public List<Emotion> Emotions;
        public List<Comment> Comments;
        public Post(string id, string title, string body, string status, string owner)
        {
            this.Id = id;
            this.Title = title;
            this.Body = body;
            this.Status = status;
            this.Owner = owner;
            this.Emotions = new List<Emotion>();
            this.Comments = new List<Comment>();
        }
    }

    public class Emotion
    {
        public string Post_id;
        public string User_id;
        public string Type;
        public Emotion(string user_id, string type)
        {
            this.User_id = user_id;
            this.Type = type;
        }
    }

    public class Comment
    {
        public string User_id;
        public string Body;
        public Comment(string user_id, string body)
        {
            this.User_id = user_id;
            this.Body = body;
        }
    }
    public static class SqlQuery
    {
        //private string SqlConnectString = @"Server=tcp:sharingplace.database.windows.net,1433;Initial Catalog=SharingPlaceDB;Persist Security Info=False;User ID=AdminSP;Password=Sharingplace@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        private const string SqlConnectString = @"Data Source=tcp:sharingplace.database.windows.net,1433;Initial Catalog=SharingPlaceDB;User ID=AdminSP;Password=Sharingplace@123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;MultiSubnetFailover=True";

        public static DataTable getData(string query)
        {
            SqlConnection sqlconnect = new SqlConnection(SqlConnectString);
            sqlconnect.Open();
            DataTable dt = new DataTable();
            SqlCommand sqlcmd = new SqlCommand(query, sqlconnect);
            sqlcmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
            try { sda.Fill(dt); }
            catch (SqlException expt)
            {
                Console.WriteLine("Query sai định dạng.\r\nErr: " + expt.ToString());
                return dt;
            }
            sqlconnect.Close();
            return dt;
        }
        public static void queryData(string query)
        {
            SqlConnection SQLConnect = new SqlConnection(SqlConnectString);
            SQLConnect.Open();
            SqlCommand cmd = new SqlCommand(query, SQLConnect);
            cmd.CommandType = CommandType.Text;
            try { cmd.ExecuteNonQuery(); }
            catch (SqlException expt) { Console.WriteLine("Query sai định dạng.\r\nErr: " + expt.ToString()); return; }
            SQLConnect.Close();
        }

    }
}
