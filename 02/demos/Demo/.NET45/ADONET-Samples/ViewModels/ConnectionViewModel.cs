using System;
using System.Data.SqlClient;
using System.Text;

namespace ADONET_Samples.ViewModels
{
  public class ConnectionViewModel : ViewModelBase
  {
    #region Constructor
    public ConnectionViewModel()
    {
      ConnectionString = AppSettings.ConnectionString;
    }
    #endregion

    public string ConnectionString { get; set; }

    #region Connect Method
    public void Connect(string cnnString)
    {
      // Create SQL Connection object
      SqlConnection cnn = new SqlConnection(cnnString);

      // Open the connection
      cnn.Open();

      // Gather connection information
      ResultText = GetConnectionInformation(cnn);

      // Close the connection
      cnn.Close();
      // Dispose of the connection
      cnn.Dispose();
    }
    #endregion

    #region ConnectUsingBlock Method
    public void ConnectUsingBlock()
    {
      // Create SQL connection object in using block for automatic closing and disposing
      using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
      {
        // Open the connection
        cnn.Open();

        ResultText = GetConnectionInformation(cnn);
      }
    }
    #endregion

    #region ConnectWithErrors Method
    public void ConnectWithErrors()
    {
      try
      {
        string cnnString = "Server=ERROR;Connection Timeout=5;Database=ERROR;UID=sa;Password=Password;Application Name=ADO.NET Samples";
        // Create SQL connection object in using block for automatic closing and disposing
        using (SqlConnection cnn = new SqlConnection(cnnString))
        {
          // Open the connection
          cnn.Open();

          ResultText = GetConnectionInformation(cnn);
        }
      }
      catch (Exception ex)
      {
        ResultText = ex.ToString();
      }
    }
    #endregion

    #region GetConnectionInformation Method
    protected virtual string GetConnectionInformation(SqlConnection cnn)
    {
      StringBuilder sb = new StringBuilder(1024);

      sb.AppendLine("Connection String: " + cnn.ConnectionString);
      sb.AppendLine("State: " + cnn.State.ToString());
      sb.AppendLine("Connection Timeout: " + cnn.ConnectionTimeout.ToString());
      sb.AppendLine("Database: " + cnn.Database);
      sb.AppendLine("Data Source: " + cnn.DataSource);
      sb.AppendLine("Server Version: " + cnn.ServerVersion);
      sb.AppendLine("Workstation ID: " + cnn.WorkstationId);

      return sb.ToString();
    }
    #endregion
  }
}
