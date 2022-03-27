using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.SQL
{
    static public class SQLCall
    {

        public static SqlDataReader ReadCall(SqlConnection sqlConnection, string commandString)
        {
            SqlCommand command = new SqlCommand(commandString, sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            return rdr;
        }

        public static SqlDataReader ReadCall(SqlConnection sqlConnection, string commandString, int id)
        {
            SqlCommand command = new SqlCommand(commandString, sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);
            sqlConnection.Open();
            SqlDataReader rdr = command.ExecuteReader();
            return rdr;
        }

        public static SqlCommand WriteCall(SqlConnection sqlConnection, string commandString)
        {
            var command = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }
    }
}