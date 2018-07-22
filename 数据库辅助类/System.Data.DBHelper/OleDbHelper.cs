using System.Configuration;
using System.Data.OleDb;

namespace System.Data.DBHelper
{
    public abstract class OleDbHelper
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static void PrepareCommand(string cmdText, CommandType cmdType, OleDbConnection conn, OleDbCommand cmd, params OleDbParameter[] commandParameters)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            cmd.Connection = conn; cmd.CommandText = cmdText; cmd.CommandType = cmdType;
            if (commandParameters != null) cmd.Parameters.AddRange(commandParameters);
        }
        public static OleDbConnection Conn { get { return new OleDbConnection(ConnectionString); } }
        public static OleDbCommand Cmd { get { return new OleDbCommand(); } }
        public static bool ExecuteNonQuery(string cmdText, CommandType cmdType, params OleDbParameter[] commandParameters)
        {
            OleDbConnection conn = Conn; OleDbCommand cmd = Cmd;
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
        public static bool ExecuteNonQueryByText(string cmdText, params OleDbParameter[] commandParameters)
        {
            return ExecuteNonQuery(cmdText, CommandType.Text, commandParameters);
        }
        public static bool ExecuteNonQueryByProc(string cmdText, params OleDbParameter[] commandParameters)
        {
            return ExecuteNonQuery(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static OleDbDataReader ExecuteReader(string cmdText, CommandType cmdType, params OleDbParameter[] commandParameters)
        {
            OleDbConnection conn = Conn; OleDbCommand cmd = Cmd;
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
        public static OleDbDataReader ExecuteReaderByText(string cmdText, params OleDbParameter[] commandParameters)
        {
            return ExecuteReader(cmdText, CommandType.Text, commandParameters);
        }
        public static OleDbDataReader ExecuteReaderByProc(string cmdText, params OleDbParameter[] commandParameters)
        {
            return ExecuteReader(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static object ExecuteScalar(string cmdText, CommandType cmdType, params OleDbParameter[] commandParameters)
        {
            OleDbConnection conn = Conn; OleDbCommand cmd = Cmd;
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
        public static object ExecuteScalarByText(string cmdText, params OleDbParameter[] commandParameters)
        {
            return ExecuteScalar(cmdText, CommandType.Text, commandParameters);
        }
        public static object ExecuteScalarByProc(string cmdText, params OleDbParameter[] commandParameters)
        {
            return ExecuteScalar(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static DataSet ExecuteDataSet(string cmdText, CommandType cmdType, params OleDbParameter[] commandParameters)
        {
            OleDbConnection conn = Conn; OleDbCommand cmd = Cmd;
            try
            {
                PrepareCommand(cmdText, cmdType, conn, cmd, commandParameters);
                DataSet ds = new DataSet(); new OleDbDataAdapter(cmd).Fill(ds); return ds;
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
        public static DataSet ExecuteDataSetByText(string cmdText, params OleDbParameter[] commandParameters)
        {
            return ExecuteDataSet(cmdText, CommandType.Text, commandParameters);
        }
        public static DataSet ExecuteDataSetByProc(string cmdText, params OleDbParameter[] commandParameters)
        {
            return ExecuteDataSet(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static DataTable ExecuteDataTable(string cmdText, CommandType cmdType, params OleDbParameter[] commandParameters)
        {
            OleDbConnection conn = Conn; OleDbCommand cmd = Cmd;
            try
            {
                PrepareCommand(cmdText, cmdType, conn, cmd, commandParameters);
                DataTable dt = new DataTable(); new OleDbDataAdapter(cmd).Fill(dt); return dt;
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
        public static DataTable ExecuteDataTableByText(string cmdText, params OleDbParameter[] commandParameters)
        {
            return ExecuteDataTable(cmdText, CommandType.Text, commandParameters);
        }
        public static DataTable ExecuteDataTableByProc(string cmdText, params OleDbParameter[] commandParameters)
        {
            return ExecuteDataTable(cmdText, CommandType.StoredProcedure, commandParameters);
        }
    }
}