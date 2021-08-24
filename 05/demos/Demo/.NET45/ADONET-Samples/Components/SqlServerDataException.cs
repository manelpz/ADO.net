using System;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ADONET_Samples
{
  public class SqlServerDataException : Exception
  {
    #region Constructors
    public SqlServerDataException() : base() { }
    public SqlServerDataException(string message) : base(message) { }
    public SqlServerDataException(string message, Exception innerException) : base(message, innerException) { }
    #endregion
    
    #region Properties
    public IDataParameterCollection CommandParameters { get; set; }
    public string SQL { get; set; }

    private string _ConnectionString = string.Empty;
    public string ConnectionString
    {
      get { return HideLoginInfoForConnectionString(_ConnectionString); }
      set { _ConnectionString = value; }
    }
    public string Database { get; set; }
    public string WorkstationId { get; set; }
    #endregion

    #region HideLoginInfoForConnectionString Method
    /// <summary>
    /// Looks for UID, User Id, Pwd, Password, etc. in a connection string and replaces their 'values' with astericks.
    /// </summary>
    /// <param name="connectString">The connection string to check</param>
    /// <returns>A string with hidden user id and password values</returns>
    protected virtual string HideLoginInfoForConnectionString(string connectionString)
    {
      SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);

      if (!string.IsNullOrEmpty(builder.UserID)) {
        builder.UserID = "******";
      }
      if (!string.IsNullOrEmpty(builder.Password)) {
        builder.Password = "******";
      }

      return builder.ConnectionString;
    }
    #endregion

    #region GetCommandParametersAsString Method
    /// <summary>
    /// Gets all parameter names and values from the Command property and returns them all as a CRLF delimited string
    /// </summary>
    /// <returns>A string with all parameter names and values</returns>
    protected virtual string GetCommandParametersAsString()
    {
      StringBuilder ret = new StringBuilder(1024);

      if (CommandParameters != null)
      {
        if (CommandParameters.Count > 0)
        {
          ret = new StringBuilder(1024);

          foreach (IDbDataParameter param in CommandParameters)
          {
            ret.Append("  " + param.ParameterName);
            if (param.Value == null)
              ret.AppendLine(" = null");
            else
              ret.AppendLine(" = " + param.Value.ToString());
          }
        }
      }

      return ret.ToString();
    }
    #endregion

    #region GetDatabaseSpecificError Method
    /// <summary>
    /// Create a string with as much specific database error as possible
    /// </summary>
    /// <param name="ex">The exception</param>
    /// <returns>A string</returns>
    protected virtual string GetDatabaseSpecificError(Exception ex)
    {
      SqlException exp;
      StringBuilder sb = new StringBuilder(1024);

      if (ex is SqlException)
      {
        exp = ((SqlException)(ex));

        for (int index = 0; index <= exp.Errors.Count - 1; index++)
        {
          sb.AppendLine();
          sb.AppendLine(new string('*', 40));
          sb.AppendLine("**** BEGIN: SQL Server Exception #" + (index + 1).ToString() + " ****");
          sb.AppendLine("            Type: " + exp.Errors[index].GetType().FullName);
          sb.AppendLine("            Message: " + exp.Errors[index].Message);
          sb.AppendLine("            Source: " + exp.Errors[index].Source);
          sb.AppendLine("            Number: " + exp.Errors[index].Number.ToString());
          sb.AppendLine("            State: " + exp.Errors[index].State.ToString());
          sb.AppendLine("            Class: " + exp.Errors[index].Class.ToString());
          sb.AppendLine("            Server: " + exp.Errors[index].Server);
          sb.AppendLine("            Procedure: " + exp.Errors[index].Procedure);
          sb.AppendLine("            LineNumber: " + exp.Errors[index].LineNumber.ToString());
          sb.AppendLine("**** END: SQL Server Exception #" + (index + 1).ToString() + " ****");
          sb.AppendLine(new string('*', 40));
        }
      }
      else
      {
        sb.Append(ex.Message);
      }

      return sb.ToString();
    }
    #endregion

    #region IsDatabaseSpecificError Method
    protected virtual bool IsDatabaseSpecificError(Exception ex)
    {
      return (ex is SqlException);
    }
    #endregion

    #region GetInnerExceptionInfo Method
    protected virtual string GetInnerExceptionInfo()
    {
      StringBuilder sb = new StringBuilder(1024);
      Exception exInner;
      int index = 1;

      exInner = InnerException;
      while (exInner != null)
      {
        if (IsDatabaseSpecificError(exInner))
        {
          sb.Append(GetDatabaseSpecificError(exInner));
        }
        else
        {
          sb.AppendLine(new string('*', 40));
          sb.AppendLine("**** BEGIN: Inner Exception #" + index.ToString() + " ****");
          sb.AppendLine("            Type: " + exInner.GetType().FullName);
          sb.AppendLine("            Message: " + exInner.Message);
          sb.AppendLine("            Source: " + exInner.Source);
          sb.AppendLine("            Number: n/a");
          sb.AppendLine("            State: n/a");
          sb.AppendLine("            Class: n/a");
          sb.AppendLine("            Server: n/a");
          sb.AppendLine("            Procedure: n/a");
          sb.AppendLine("            LineNumber: n/a");
          sb.AppendLine("**** END: Inner Exception #" + index.ToString() + "****");
          sb.AppendLine(new string('*', 40));
        }
        index++;

        // Get next inner exception
        exInner = exInner.InnerException;
      }

      return sb.ToString();
    }
    #endregion

    #region Override of ToString Method
    /// <summary>
    /// Gathers all information from the exception information gathered and returns a string
    /// </summary>
    /// <returns>A database specific error string</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder(1024);

      sb.AppendLine(new string('-', 80));
      if (!string.IsNullOrEmpty(Message))
      {
        sb.AppendLine("Type: " + this.GetType().FullName);
      }
      if (!string.IsNullOrEmpty(Message))
      {
        sb.AppendLine("Message: " + Message);
      }
      if (!string.IsNullOrEmpty(Database))
      {
        sb.AppendLine("Database: " + Database);
      }
      if (!string.IsNullOrEmpty(SQL))
      {
        sb.AppendLine("SQL: " + SQL);
      }
      if (CommandParameters.Count > 0)
      {
        sb.AppendLine("Parameters:");
        sb.Append(GetCommandParametersAsString());
      }
      if (!string.IsNullOrEmpty(ConnectionString))
      {
        sb.AppendLine("Connection String: " + ConnectionString);
      }
      if (!string.IsNullOrEmpty(StackTrace))
      {
        sb.AppendLine("Stack Trace: " + StackTrace);
      }
      if (IsDatabaseSpecificError(this))
      {
        sb.AppendLine(GetDatabaseSpecificError(this));
      }
      // Gather info from inner exceptions
      sb.AppendLine(GetInnerExceptionInfo());
      // Report stack trace
      sb.AppendLine("Stack Trace: " + this.InnerException.StackTrace);

      return sb.ToString();
    }
    #endregion
  }
}
