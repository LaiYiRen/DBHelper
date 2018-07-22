using System.Configuration;
using System.Data.Odbc;

namespace System.Data.DBHelper
{
    public abstract class OdbcHelper
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static void PrepareCommand(string cmdText, CommandType cmdType, OdbcConnection conn, OdbcCommand cmd, params OdbcParameter[] commandParameters)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            cmd.Connection = conn; cmd.CommandText = cmdText; cmd.CommandType = cmdType;
            if (commandParameters != null) cmd.Parameters.AddRange(commandParameters);
        }
        public static OdbcConnection Conn { get { return new OdbcConnection(ConnectionString); } }
        public static OdbcCommand Cmd { get { return new OdbcCommand(); } }
        public static bool ExecuteNonQuery(string cmdText, CommandType cmdType, params OdbcParameter[] commandParameters)
        {
            OdbcConnection conn = Conn; OdbcCommand cmd = Cmd;
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
        public static bool ExecuteNonQueryByText(string cmdText, params OdbcParameter[] commandParameters)
        {
            return ExecuteNonQuery(cmdText, CommandType.Text, commandParameters);
        }
        public static bool ExecuteNonQueryByProc(string cmdText, params OdbcParameter[] commandParameters)
        {
            return ExecuteNonQuery(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static OdbcDataReader ExecuteReader(string cmdText, CommandType cmdType, params OdbcParameter[] commandParameters)
        {
            OdbcConnection conn = Conn; OdbcCommand cmd = Cmd;
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
        public static OdbcDataReader ExecuteReaderByText(string cmdText, params OdbcParameter[] commandParameters)
        {
            return ExecuteReader(cmdText, CommandType.Text, commandParameters);
        }
        public static OdbcDataReader ExecuteReaderByProc(string cmdText, params OdbcParameter[] commandParameters)
        {
            return ExecuteReader(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static object ExecuteScalar(string cmdText, CommandType cmdType, params OdbcParameter[] commandParameters)
        {
            OdbcConnection conn = Conn; OdbcCommand cmd = Cmd;
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
        public static object ExecuteScalarByText(string cmdText, params OdbcParameter[] commandParameters)
        {
            return ExecuteScalar(cmdText, CommandType.Text, commandParameters);
        }
        public static object ExecuteScalarByProc(string cmdText, params OdbcParameter[] commandParameters)
        {
            return ExecuteScalar(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static DataSet ExecuteDataSet(string cmdText, CommandType cmdType, params OdbcParameter[] commandParameters)
        {
            OdbcConnection conn = Conn; OdbcCommand cmd = Cmd;
            try
            {
                PrepareCommand(cmdText, cmdType, conn, cmd, commandParameters);
                DataSet ds = new DataSet(); new OdbcDataAdapter(cmd).Fill(ds); return ds;
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
        public static DataSet ExecuteDataSetByText(string cmdText, params OdbcParameter[] commandParameters)
        {
            return ExecuteDataSet(cmdText, CommandType.Text, commandParameters);
        }
        public static DataSet ExecuteDataSetByProc(string cmdText, params OdbcParameter[] commandParameters)
        {
            return ExecuteDataSet(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static DataTable ExecuteDataTable(string cmdText, CommandType cmdType, params OdbcParameter[] commandParameters)
        {
            OdbcConnection conn = Conn; OdbcCommand cmd = Cmd;
            try
            {
                PrepareCommand(cmdText, cmdType, conn, cmd, commandParameters);
                DataTable dt = new DataTable(); new OdbcDataAdapter(cmd).Fill(dt); return dt;
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
        public static DataTable ExecuteDataTableByText(string cmdText, params OdbcParameter[] commandParameters)
        {
            return ExecuteDataTable(cmdText, CommandType.Text, commandParameters);
        }
        public static DataTable ExecuteDataTableByProc(string cmdText, params OdbcParameter[] commandParameters)
        {
            return ExecuteDataTable(cmdText, CommandType.StoredProcedure, commandParameters);
        }
    }
}