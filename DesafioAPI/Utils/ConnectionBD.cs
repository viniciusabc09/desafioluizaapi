using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAPI.Utils
{
    public static class ConnectionBD
    {
        public static string ReturnConection()
        {
            string servidor = "127.0.0.1";
            string database = "desafioluiza";
            string usuario  = "root";
            string senha = "root";
            string conexao = "Server=" +servidor+ "; port=3306; Database=" + database+ ";Uid="+ usuario + ";Pwd="+senha;

            return conexao;
        }

        public static bool InsertUpdateDB(string query)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(ReturnConection());
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand(query, conexao);
                cmd.ExecuteNonQuery();
                conexao.Close();
                return true;
            }
            catch (Exception e)
            {
                var t = e.Message;
                return false;
            }
        }

        public static DataTable ReturnDataDB(string query)
        {
            DataTable dt = new DataTable();
            MySqlConnection conexao = new MySqlConnection(ReturnConection());
            conexao.Open();
            MySqlCommand cmd = new MySqlCommand(query, conexao);
            MySqlDataReader reader;
            reader = cmd.ExecuteReader();
            dt.Load(reader);
            conexao.Close();
            return dt;
        }
    }
}
