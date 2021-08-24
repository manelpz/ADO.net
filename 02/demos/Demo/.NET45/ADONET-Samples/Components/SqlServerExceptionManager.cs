using System;
using System.Data.SqlClient;

namespace ADONET_Samples
{
  public class SqlServerExceptionManager : CommonBase
  {
    #region Instance Property
    private static SqlServerExceptionManager _Instance;

    public static SqlServerExceptionManager Instance
    {
      get {
        if (_Instance == null) {
          _Instance = new SqlServerExceptionManager();
        }

        return _Instance;
      }
      set { _Instance = value; }
    }
    #endregion

    /// <summary>
    /// Get/Set Last Exception Object Created
    /// </summary>
    public Exception LastException { get; set; }

    #region Publish Methods
    public virtual void Publish(Exception ex)
    {
      LastException = ex;

      // TODO: Implement an exception publisher here
      System.Diagnostics.Debug.WriteLine(ex.ToString());
    }

    public virtual void Publish(Exception ex, SqlCommand cmd)
    {
      Publish(ex, cmd, null);
    }

    public virtual void Publish(Exception ex, SqlCommand cmd, string exceptionMsg)
    {
      LastException = ex;

      if (cmd != null)
      {
        LastException = CreateDbException(ex, cmd, null);

        // TODO: Implement an exception publisher here
        System.Diagnostics.Debug.WriteLine(ex.ToString());
      }
    }
    #endregion

    #region CreateDbException Method
    public virtual SqlServerDataException CreateDbException(Exception ex, SqlCommand cmd, string exceptionMsg)
    {
      SqlServerDataException exc;
      exceptionMsg = string.IsNullOrEmpty(exceptionMsg) ? string.Empty : exceptionMsg + " - ";

      exc = new SqlServerDataException(exceptionMsg + ex.Message, ex)
      {
        ConnectionString = cmd.Connection.ConnectionString,
        Database = cmd.Connection.Database,
        SQL = cmd.CommandText,
        CommandParameters = cmd.Parameters,
        WorkstationId = Environment.MachineName
      };

      return exc;
    }
    #endregion
  }
}
