using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Sharing_Place.Models
{
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
