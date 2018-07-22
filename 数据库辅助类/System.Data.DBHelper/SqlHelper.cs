using System.Configuration;
using System.Data.SqlClient;

namespace System.Data.DBHelper
{
    public abstract class SqlHelper
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static void PrepareCommand(string cmdText, CommandType cmdType, SqlConnection conn, SqlCommand cmd, params SqlParameter[] commandParameters)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            cmd.Connection = conn; cmd.CommandText = cmdText; cmd.CommandType = cmdType;
            if (commandParameters != null) cmd.Parameters.AddRange(commandParameters);
        }
        public static SqlConnection Conn { get { return new SqlConnection(ConnectionString); } }
        public static SqlCommand Cmd { get { return new SqlCommand(); } }
        public static bool ExecuteNonQuery(string cmdText, CommandType cmdType, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = Conn; SqlCommand cmd = Cmd;
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
        public static bool ExecuteNonQueryByText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(cmdText, CommandType.Text, commandParameters);
        }
        public static bool ExecuteNonQueryByProc(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static SqlDataReader ExecuteReader(string cmdText, CommandType cmdType, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = Conn; SqlCommand cmd = Cmd;
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
        public static SqlDataReader ExecuteReaderByText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(cmdText, CommandType.Text, commandParameters);
        }
        public static SqlDataReader ExecuteReaderByProc(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static object ExecuteScalar(string cmdText, CommandType cmdType, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = Conn; SqlCommand cmd = Cmd;
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
        public static object ExecuteScalarByText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(cmdText, CommandType.Text, commandParameters);
        }
        public static object ExecuteScalarByProc(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static DataSet ExecuteDataSet(string cmdText, CommandType cmdType, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = Conn; SqlCommand cmd = Cmd;
            try
            {
                PrepareCommand(cmdText, cmdType, conn, cmd, commandParameters);
                DataSet ds = new DataSet(); new SqlDataAdapter(cmd).Fill(ds); return ds;
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
        public static DataSet ExecuteDataSetByText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(cmdText, CommandType.Text, commandParameters);
        }
        public static DataSet ExecuteDataSetByProc(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(cmdText, CommandType.StoredProcedure, commandParameters);
        }
        public static DataTable ExecuteDataTable(string cmdText, CommandType cmdType, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = Conn; SqlCommand cmd = Cmd;
            try
            {
                PrepareCommand(cmdText, cmdType, conn, cmd, commandParameters);
                DataTable dt = new DataTable(); new SqlDataAdapter(cmd).Fill(dt); return dt;
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
        public static DataTable ExecuteDataTableByText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataTable(cmdText, CommandType.Text, commandParameters);
        }
        public static DataTable ExecuteDataTableByProc(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataTable(cmdText, CommandType.StoredProcedure, commandParameters);
        }
    }
}