using System;
using System.Data;
using System.Data.SqlClient;

namespace ADONET_Samples.ViewModels
{
  public class ExceptionViewModel : ViewModelBase
  {
    #region SimpleExceptionHandling Method
    public int SimpleExceptionHandling()
    {
      try {
        // Create SQL statement to submit
        string sql = "INSERT INTO Product(ProductName, IntroductionDate, Url, Price)";
        sql += " VALUES('VB.NET Fundamentals', '2019-05-21', 'https://bit.ly/30KKHjs', 19.99)";

        // Create SQL connection object in using block for automatic closing and disposing
        using (SqlConnection cnn = new SqlConnection("Blah Blah")) {
          // Open the connection
          cnn.Open();

          // Create command object in using block for automatic disposal
          using (SqlCommand cmd = new SqlCommand(sql, cnn)) {
            // Set CommandType
            cmd.CommandType = CommandType.Text;
            // Execute the INSERT statement
            RowsAffected = cmd.ExecuteNonQuery();

            ResultText = "Rows Affected: " + RowsAffected.ToString();
          }
        }
      }
      catch (Exception ex) {
        // NOTE: A problem here is no access to SqlCommand object
        //       Thus, we have lost the SQL statement and 
        //       connection information for reporting purpose
        ResultText = ex.ToString();
      }

      return RowsAffected;
    }
    #endregion

    #region CatchSqlException Method
    public int CatchSqlException()
    {
      try {
        // Create SQL statement to submit
        string sql = "Product_Insert";

        // Create SQL connection object in using block for automatic closing and disposing
        using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString)) {
          cnn.Open();

          // Create command object in using block for automatic disposal
          using (SqlCommand cmd = new SqlCommand(sql, cnn)) {
            // NOTE: The following parameter is spelled incorrectly.
            cmd.Parameters.Add(new SqlParameter("@ProdutName", "Generate Exception"));
            cmd.Parameters.Add(new SqlParameter("@IntroductionDate", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@Url", "www.wrong.com"));
            cmd.Parameters.Add(new SqlParameter("@Price", 0));

            // Set CommandType
            cmd.CommandType = CommandType.StoredProcedure;
            // Execute the INSERT statement
            RowsAffected = cmd.ExecuteNonQuery();

            ResultText = "Rows Affected: " + RowsAffected.ToString();
          }
        }
      }
      catch (SqlException ex) {
        // Get all error information
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int i = 0; i < ex.Errors.Count; i++) {
          sb.AppendLine("Index #: " + i.ToString());
          sb.AppendLine("Type: " + ex.Errors[i].GetType().FullName);
          sb.AppendLine("Message: " + ex.Errors[i].Message);
          sb.AppendLine("Source: " + ex.Errors[i].Source);
          sb.AppendLine("Number: " + ex.Errors[i].Number.ToString());
          sb.AppendLine("State: " + ex.Errors[i].State.ToString());
          sb.AppendLine("Class: " + ex.Errors[i].Class.ToString());
          sb.AppendLine("Server: " + ex.Errors[i].Server);
          sb.AppendLine("Procedure: " + ex.Errors[i].Procedure);
          sb.AppendLine("LineNumber: " + ex.Errors[i].LineNumber.ToString());
        }

        // NOTE: A problem here is no access to SqlCommand object
        //       Thus, we have lost the SQL statement and 
        //       connection information for reporting purpose
        ResultText = sb.ToString() + Environment.NewLine + ex.ToString();
      }
      catch (Exception ex) {
        ResultText = ex.ToString();
      }

      return RowsAffected;
    }
    #endregion

    #region GatherExceptionInformation Method
    public int GatherExceptionInformation()
    {
      // Define Command object outside of try...catch block 
      // so you can gather more information for exception handling
      SqlCommand cmd = null;

      try {
        // Create SQL statement to submit
        string sql = "INSERT INTO Product(ProductName, IntroductionDate, Url, Price)";
        sql += " VALUES(@ProductName, @IntroductionDate, @Url, @Price)";

        // Create SQL connection object in using block for automatic closing and disposing
        using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString)) {
          cnn.Open();

          // Create command object in using block for automatic disposal
          using (cmd = new SqlCommand(sql, cnn)) {
            // NOTE: The following parameter is spelled incorrectly.
            cmd.Parameters.Add(new SqlParameter("@ProdutName", "Generate Exception"));
            cmd.Parameters.Add(new SqlParameter("@IntroductionDate", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@Url", "www.wrong.com"));
            cmd.Parameters.Add(new SqlParameter("@Price", 0));

            // Set CommandType
            cmd.CommandType = CommandType.Text;
            // Execute the INSERT statement
            RowsAffected = cmd.ExecuteNonQuery();

            ResultText = "Rows Affected: " + RowsAffected.ToString();
          }
        }
      }
      catch (SqlException ex) {
        SqlServerExceptionManager.Instance.Publish(ex, cmd, "Error in ExceptionViewModel.GatherExceptionInformation()");
        ResultText = SqlServerExceptionManager.Instance.LastException.ToString();
      }
      catch (Exception ex) {
        ResultText = ex.ToString();
      }

      return RowsAffected;
    }
    #endregion
  }
}
