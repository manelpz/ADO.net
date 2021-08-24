using ADONET_Samples.ManagerClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ADONET_Samples.ViewModels
{
  public class BuilderViewModel : ViewModelBase
  {
    #region Constructor
    public BuilderViewModel()
    {
      ConnectionString = AppSettings.ConnectionString;
    }
    #endregion

    public string ConnectionString { get; set; }

    #region BreakApartConnectionString Method
    public string BreakApartConnectionString()
    {
      StringBuilder sb = new StringBuilder(1024);

      // Create a connection string builder object
      SqlConnectionStringBuilder builder = 
        new SqlConnectionStringBuilder(ConnectionString);
     
      // Access each property of the connection string
      sb.AppendLine("Application Name: " + builder.ApplicationName);
      sb.AppendLine("Data Source: " + builder.DataSource);
      sb.AppendLine("Initial Catalog: " + builder.InitialCatalog);
      sb.AppendLine("User ID: " + builder.UserID);
      sb.AppendLine("Password: " + builder.Password);
      sb.AppendLine("Integrated Security: " + builder.IntegratedSecurity);

      ResultText = sb.ToString();

      return ResultText;
    }
    #endregion

    #region CreateConnectionString Method
    public string CreateConnectionString()
    {
      SqlConnectionStringBuilder builder =
        new SqlConnectionStringBuilder
      {
        ApplicationName = "A New Application",
        ConnectTimeout = 5,
        DataSource = "Localhost",
        InitialCatalog = "ADONETSamples",
        UserID = "Tester",
        Password = "P@ssw0rd"
      };

      ResultText = builder.ToString();

      return ResultText;
    }
    #endregion

    #region CreateDataModificationCommands Method
    public string CreateDataModificationCommands()
    {
      try
      {
        // Create SQL connection object
        using (SqlConnection cnn =
          new SqlConnection(AppSettings.ConnectionString))
        {
          // Create a SQL Data Adapter
          using (SqlDataAdapter da = 
            new SqlDataAdapter(ProductManager.PRODUCT_CATEGORY_SQL, cnn))
          {
            // Fill DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Create a command builder
            using (SqlCommandBuilder builder = 
              new SqlCommandBuilder(da))
            {
              // Build INSERT Command
              // Pass true to generate parameters names matching column names
              ResultText = "Insert: " + builder.GetInsertCommand(true).CommandText;

              ResultText += Environment.NewLine;

              // Build UPDATE command
              ResultText += "Update: " + builder.GetUpdateCommand(true).CommandText;

              ResultText += Environment.NewLine;

              // Build DELETE command
              ResultText += "Delete: " + builder.GetDeleteCommand(true).CommandText;              
            }
          }
        }
      }
      catch (Exception ex)
      {
        ResultText = ex.ToString();
      }

      return ResultText;
    }
    #endregion

    #region InsertUsingDataModificationCommand Method
    public string InsertUsingDataModificationCommand()
    {
      try
      {
        // Create SQL connection object
        using (SqlConnection cnn = 
          new SqlConnection(AppSettings.ConnectionString))
        {
          // Create a SQL Data Adapter
          using (SqlDataAdapter da = 
            new SqlDataAdapter(ProductManager.PRODUCT_SQL, cnn))
          {
            // Fill DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Create a command builder
            using (SqlCommandBuilder builder = 
              new SqlCommandBuilder(da))
            {
              // Build INSERT Command
              using (SqlCommand cmd = builder.GetInsertCommand(true))
              {
                // Set generated parameters with values to insert
                cmd.Parameters["@ProductName"].Value = "A New Product 123";
                cmd.Parameters["@IntroductionDate"].Value = DateTime.Now;
                cmd.Parameters["@Url"].Value = "www.fairwaytech.com";
                cmd.Parameters["@Price"].Value = 100;
                cmd.Parameters["@RetireDate"].Value = DateTime.Now.AddMonths(3);
                cmd.Parameters["@ProductCategoryId"].Value = 1;

                // Set the connection into the command object
                cmd.Connection = cnn;

                // Open the connection for inserting
                cnn.Open();

                // Execute the command
                cmd.ExecuteNonQuery();
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        ResultText = ex.ToString();
      }

      return ResultText;
    }
    #endregion
  }
}
