using System.Configuration;
using System.Data.SQLite;

namespace System.Data.DBHelper
{
    public abstract class SQLiteHelper
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static void PrepareCommand(string cmdText, CommandType cmdType, SQLiteConnection conn, SQLiteCommand cmd, params SQLiteParameter[] commandParameters)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            cmd.Connection = conn; cmd.CommandText = cmdText; cmd.CommandType = cmdType;
            if (commandParameters != null) cmd.Parameters.AddRange(commandParameters);
        }
        public static SQLiteConnection Conn { get { return new SQLiteConnection(ConnectionString); } }
        public static SQLiteCommand Cmd { get { return new SQLiteCommand(); } }
        public static bool ExecuteNonQuery(string cmdText, CommandType cmdType, params SQLiteParameter[] commandParameters)
        {
            SQLiteConnection conn = Conn; SQLiteCommand cmd = Cmd;
            try
            {
                PrepareCommand(cmdText, cmdType, conn, cmd, commandParameters);
                return cmd.ExecuteNonQuery() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                conn.Close(); throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close(); cmd.Parameters.Clear();
            }
        }
        public static bool ExecuteNonQueryByText(string cmdText, params SQLiteParameter[] commandParameters)
        {
            return ExecuteNonQuery(cmdText, CommandType.Text, commandParameters);
        }
        public static bool ExecuteNonQueryByProc(string cmdText, params SQLiteParameter[] commandParameters)
        {
            return ExecuteNonQuery(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static SQLiteDataReader ExecuteReader(string cmdText, CommandType cmdType, params SQLiteParameter[] commandParameters)
        {
            SQLiteConnection conn = Conn; SQLiteCommand cmd = Cmd;
            try
            {
                PrepareCommand(cmdText, cmdType, conn, cmd, commandParameters);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                conn.Close(); throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
            }
        }
        public static SQLiteDataReader ExecuteReaderByText(string cmdText, params SQLiteParameter[] commandParameters)
        {
            return ExecuteReader(cmdText, CommandType.Text, commandParameters);
        }
        public static SQLiteDataReader ExecuteReaderByProc(string cmdText, params SQLiteParameter[] commandParameters)
        {
            return ExecuteReader(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static object ExecuteScalar(string cmdText, CommandType cmdType, params SQLiteParameter[] commandParameters)
        {
            SQLiteConnection conn = Conn; SQLiteCommand cmd = Cmd;
            try
            {
                PrepareCommand(cmdText, cmdType, conn, cmd, commandParameters);
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                conn.Close(); throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close(); cmd.Parameters.Clear();
            }
        }
        public static object ExecuteScalarByText(string cmdText, params SQLiteParameter[] commandParameters)
        {
            return ExecuteScalar(cmdText, CommandType.Text, commandParameters);
        }
        public static object ExecuteScalarByProc(string cmdText, params SQLiteParameter[] commandParameters)
        {
            return ExecuteScalar(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static DataSet ExecuteDataSet(string cmdText, CommandType cmdType, params SQLiteParameter[] commandParameters)
        {
            SQLiteConnection conn = Conn; SQLiteCommand cmd = Cmd;
            try
            {
                PrepareCommand(cmdText, cmdType, conn, cmd, commandParameters);
                DataSet ds = new DataSet(); new SQLiteDataAdapter(cmd).Fill(ds); return ds;
            }
            catch (Exception ex)
            {
                conn.Close(); throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close(); cmd.Parameters.Clear();
            }
        }
        public static DataSet ExecuteDataSetByText(string cmdText, params SQLiteParameter[] commandParameters)
        {
            return ExecuteDataSet(cmdText, CommandType.Text, commandParameters);
        }
        public static DataSet ExecuteDataSetByProc(string cmdText, params SQLiteParameter[] commandParameters)
        {
            return ExecuteDataSet(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static DataTable ExecuteDataTable(string cmdText, CommandType cmdType, params SQLiteParameter[] commandParameters)
        {
            SQLiteConnection conn = Conn; SQLiteCommand cmd = Cmd;
            try
            {
                PrepareCommand(cmdText, cmdType, conn, cmd, commandParameters);
                DataTable dt = new DataTable(); new SQLiteDataAdapter(cmd).Fill(dt); return dt;
            }
            catch (Exception ex)
            {
                conn.Close(); throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close(); cmd.Parameters.Clear();
            }
        }
        public static DataTable ExecuteDataTableByText(string cmdText, params SQLiteParameter[] commandParameters)
        {
            return ExecuteDataTable(cmdText, CommandType.Text, commandParameters);
        }
        public static DataTable ExecuteDataTableByProc(string cmdText, params SQLiteParameter[] commandParameters)
        {
            return ExecuteDataTable(cmdText, CommandType.StoredProcedure, commandParameters);
        }
    }
}