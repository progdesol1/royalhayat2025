#region Assembly SaasHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// D:\DigitalSol\DigitalRoyalHayat\RoyalHayat\Classes\bin\Debug\SaasHelper.dll
// Decompiled with ICSharpCode.Decompiler 8.2.0.7535
#endregion

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml;



public sealed class SqlHelper
{
    private enum SqlConnectionOwnership
    {
        Internal,
        External
    }

    private SqlHelper()
    {
    }

    private static string SetConnection()
    {
        return ConfigurationManager.ConnectionStrings["Saas"].ConnectionString;
    }

    private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
    {
        foreach (SqlParameter sqlParameter in commandParameters)
        {
            if (sqlParameter != null)
            {
                if (sqlParameter.Direction == ParameterDirection.InputOutput && sqlParameter.Value == null)
                {
                    sqlParameter.Value = DBNull.Value;
                }

                command.Parameters.Add(sqlParameter);
            }
        }
    }

    private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
    {
        if (commandParameters != null && parameterValues != null)
        {
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            int i = 0;
            for (int num = commandParameters.Length; i < num; i++)
            {
                commandParameters[i].Value = parameterValues[i];
            }
        }
    }

    private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
    {
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        command.Connection = connection;
        command.CommandText = commandText;
        if (transaction != null)
        {
            command.Transaction = transaction;
        }

        command.CommandType = commandType;
        if (commandParameters != null)
        {
            AttachParameters(command, commandParameters);
        }
    }

    public static int ExecuteDeleteQuery(string connectionString, string deleteText, int userID)
    {
        string text = deleteText.Trim().ToUpper();
        if (0 != text.IndexOf("DELETE"))
        {
            return -1;
        }

        int num = text.IndexOf("FROM") + "FROM ".Length;
        int num2 = text.IndexOf("WHERE");
        string text2 = text.Substring(num, num2 - num);
        string commandText = "UPDATE " + text2 + " SET [TimeStamp] = GetDate() AND [User_ID] = @UserID";
        SqlParameter sqlParameter = new SqlParameter("@UserID", userID);
        ExecuteNonQuery(connectionString, CommandType.Text, commandText, sqlParameter);
        return ExecuteNonQuery(connectionString, CommandType.Text, deleteText, sqlParameter);
    }

    public static int ExecuteDeleteQuery(SqlTransaction transaction, string deleteText, int userID)
    {
        string text = deleteText.Trim().ToUpper();
        if (0 != text.IndexOf("DELETE"))
        {
            return -1;
        }

        int num = text.IndexOf("FROM") + "FROM ".Length;
        int num2 = text.IndexOf("WHERE");
        string text2 = text.Substring(num, num2 - num);
        string text3 = "UPDATE " + text2 + " SET [TimeStamp] = GetDate() AND [User_ID] = @UserID";
        SqlParameter sqlParameter = new SqlParameter("@UserID", userID);
        return ExecuteNonQuery(transaction, CommandType.Text, deleteText, sqlParameter);
    }

    public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
    {
        return ExecuteNonQuery(connectionString, commandType, commandText, (SqlParameter[])null);
    }

    public static int ExecuteNonQuery(CommandType commandType, string commandText)
    {
        return ExecuteNonQuery(SetConnection(), commandType, commandText, (SqlParameter[])null);
    }

    public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
         SqlConnection sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();
        return ExecuteNonQuery(sqlConnection, commandType, commandText, commandParameters);
    }

    public static int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
         SqlConnection sqlConnection = new SqlConnection(SetConnection());
        sqlConnection.Open();
        return ExecuteNonQuery(sqlConnection, commandType, commandText, commandParameters);
    }

    //public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
    //{
    //    if (parameterValues != null && parameterValues.Length > 0)
    //    {
    //        SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
    //        AssignParameterValues(spParameterSet, parameterValues);
    //        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
    //    }

    //    return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
    //}

    //public static int ExecuteNonQuery(string spName, params object[] parameterValues)
    //{
    //    if (parameterValues != null && parameterValues.Length > 0)
    //    {
    //        SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(SetConnection(), spName);
    //        AssignParameterValues(spParameterSet, parameterValues);
    //        return ExecuteNonQuery(SetConnection(), CommandType.StoredProcedure, spName, spParameterSet);
    //    }

    //    return ExecuteNonQuery(SetConnection(), CommandType.StoredProcedure, spName);
    //}

    public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
    {
        return ExecuteNonQuery(connection, commandType, commandText, (SqlParameter[])null);
    }

    public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand sqlCommand = new SqlCommand();
        PrepareCommand(sqlCommand, connection, null, commandType, commandText, commandParameters);
        int result = sqlCommand.ExecuteNonQuery();
        sqlCommand.Parameters.Clear();
        return result;
    }

   
    public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
    {
        return ExecuteNonQuery(transaction, commandType, commandText, (SqlParameter[])null);
    }

    public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand sqlCommand = new SqlCommand();
        PrepareCommand(sqlCommand, transaction.Connection, transaction, commandType, commandText, commandParameters);
        int result = sqlCommand.ExecuteNonQuery();
        sqlCommand.Parameters.Clear();
        return result;
    }

    //public static int ExecuteNonQuery(SqlTransaction transaction, string spName, params object[] parameterValues)
    //{
    //    if (parameterValues != null && parameterValues.Length > 0)
    //    {
    //        SqlParameter[] spParameterSet = SqlParameter.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
    //        AssignParameterValues(spParameterSet, parameterValues);
    //        return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParameterSet);
    //    }

    //    return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
    //}

    public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
    {
        return ExecuteDataset(connectionString, commandType, commandText, (SqlParameter[])null);
    }

    public static DataSet ExecuteDataset(CommandType commandType, string commandText)
    {
        return ExecuteDataset(SetConnection(), commandType, commandText, (SqlParameter[])null);
    }

    public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
         SqlConnection sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();
        return ExecuteDataset(sqlConnection, commandType, commandText, commandParameters);
    }

    public static DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
         SqlConnection sqlConnection = new SqlConnection(SetConnection());
        sqlConnection.Open();
        return ExecuteDataset(sqlConnection, commandType, commandText, commandParameters);
    }

  

    public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)
    {
        return ExecuteDataset(connection, commandType, commandText, (SqlParameter[])null);
    }

    public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand sqlCommand = new SqlCommand();
        PrepareCommand(sqlCommand, connection, null, commandType, commandText, commandParameters);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
        DataSet dataSet = new DataSet();
        sqlCommand.CommandTimeout = 400;
        sqlDataAdapter.Fill(dataSet);
        sqlCommand.Parameters.Clear();
        return dataSet;
    }

    public static int UpdateDataset(DataSet ds, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
         SqlConnection sqlConnection = new SqlConnection(SetConnection());
        sqlConnection.Open();
        SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
        UpdateDataset(ds, sqlConnection, commandType, commandText, commandParameters);
        sqlTransaction.Commit();
        return 1;
    }

    public static int UpdateDataset(DataSet ds, SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand sqlCommand = new SqlCommand();
        PrepareCommand(sqlCommand, connection, null, commandType, commandText, commandParameters);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        sqlDataAdapter.UpdateCommand = sqlCommand;
        sqlDataAdapter.Update(ds.Tables[0]);
        sqlCommand.Parameters.Clear();
        return 1;
    }

 

    public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText)
    {
        return ExecuteDataset(transaction, commandType, commandText, (SqlParameter[])null);
    }

    public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand sqlCommand = new SqlCommand();
        PrepareCommand(sqlCommand, transaction.Connection, transaction, commandType, commandText, commandParameters);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
        DataSet dataSet = new DataSet();
        sqlDataAdapter.Fill(dataSet);
        sqlCommand.Parameters.Clear();
        return dataSet;
    }

  

    public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText)
    {
        return ExecuteDataset(connectionString, commandType, commandText, (SqlParameter[])null).Tables[0];
    }

    public static DataTable ExecuteDataTable(CommandType commandType, string commandText)
    {
        return ExecuteDataset(SetConnection(), commandType, commandText, (SqlParameter[])null).Tables[0];
    }

    public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
         SqlConnection sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();
        return ExecuteDataset(sqlConnection, commandType, commandText, commandParameters).Tables[0];
    }

    public static DataTable ExecuteDataTable(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
         SqlConnection sqlConnection = new SqlConnection(SetConnection());
        sqlConnection.Open();
        return ExecuteDataset(sqlConnection, commandType, commandText, commandParameters).Tables[0];
    }

    private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, SqlConnectionOwnership connectionOwnership)
    {
        SqlCommand sqlCommand = new SqlCommand();
        PrepareCommand(sqlCommand, connection, transaction, commandType, commandText, commandParameters);
        SqlDataReader result = ((connectionOwnership != SqlConnectionOwnership.External) ? sqlCommand.ExecuteReader(CommandBehavior.CloseConnection) : sqlCommand.ExecuteReader());
        sqlCommand.Parameters.Clear();
        return result;
    }

    public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
    {
        return ExecuteReader(connectionString, commandType, commandText, (SqlParameter[])null);
    }

    public static SqlDataReader ExecuteReader(CommandType commandType, string commandText)
    {
        return ExecuteReader(SetConnection(), commandType, commandText, (SqlParameter[])null);
    }

    public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();
        try
        {
            return ExecuteReader(sqlConnection, null, commandType, commandText, commandParameters, SqlConnectionOwnership.Internal);
        }
        catch
        {
            sqlConnection.Close();
            throw;
        }
    }

    public static SqlDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlConnection sqlConnection = new SqlConnection(SetConnection());
        sqlConnection.Open();
        try
        {
            return ExecuteReader(sqlConnection, null, commandType, commandText, commandParameters, SqlConnectionOwnership.Internal);
        }
        catch
        {
            sqlConnection.Close();
            throw;
        }
    }

   

    public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
    {
        return ExecuteReader(connection, commandType, commandText, (SqlParameter[])null);
    }

    public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        return ExecuteReader(connection, null, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
    }


    public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText)
    {
        return ExecuteReader(transaction, commandType, commandText, (SqlParameter[])null);
    }

    public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
    }

  

    public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
    {
        return ExecuteScalar(connectionString, commandType, commandText, (SqlParameter[])null);
    }

    public static object ExecuteScalar(CommandType commandType, string commandText)
    {
        return ExecuteScalar(SetConnection(), commandType, commandText, (SqlParameter[])null);
    }

    public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
         SqlConnection sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();
        return ExecuteScalar(sqlConnection, commandType, commandText, commandParameters);
    }

  

    public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
    {
        return ExecuteScalar(connection, commandType, commandText, (SqlParameter[])null);
    }

    public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand sqlCommand = new SqlCommand();
        PrepareCommand(sqlCommand, connection, null, commandType, commandText, commandParameters);
        object result = sqlCommand.ExecuteScalar();
        sqlCommand.Parameters.Clear();
        return result;
    }

  

    public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
    {
        return ExecuteScalar(transaction, commandType, commandText, (SqlParameter[])null);
    }

    public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand sqlCommand = new SqlCommand();
        PrepareCommand(sqlCommand, transaction.Connection, transaction, commandType, commandText, commandParameters);
        object result = sqlCommand.ExecuteScalar();
        sqlCommand.Parameters.Clear();
        return result;
    }

    

    public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText)
    {
        return ExecuteXmlReader(connection, commandType, commandText, (SqlParameter[])null);
    }

    public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand sqlCommand = new SqlCommand();
        PrepareCommand(sqlCommand, connection, null, commandType, commandText, commandParameters);
        XmlReader result = sqlCommand.ExecuteXmlReader();
        sqlCommand.Parameters.Clear();
        return result;
    }

   

    public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText)
    {
        return ExecuteXmlReader(transaction, commandType, commandText, (SqlParameter[])null);
    }

    public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand sqlCommand = new SqlCommand();
        PrepareCommand(sqlCommand, transaction.Connection, transaction, commandType, commandText, commandParameters);
        XmlReader result = sqlCommand.ExecuteXmlReader();
        sqlCommand.Parameters.Clear();
        return result;
    }

    
}
#if false // Decompilation log
'31' items in cache
------------------
Resolve: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\mscorlib.dll'
------------------
Resolve: 'System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Data.dll'
------------------
Resolve: 'System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Xml.dll'
------------------
Resolve: 'System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Configuration.dll'
#endif
