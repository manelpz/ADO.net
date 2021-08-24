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
      SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(AppSettings.ConnectionString);
      StringBuilder sb = new StringBuilder(1024);

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
      SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
      {
        ApplicationName = "A New Application",
        ConnectTimeout = 5,
        DataSource = "Localhost",
        InitialCatalog = "ADONETSamples",
        UserID = "saUser",
        Password = "P@ssw0rd!2019"
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
        // Create SQL connection object in using block for automatic closing and disposing
        using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
        {
          // Create a SQL Data Adapter
          using (SqlDataAdapter da = new SqlDataAdapter(ProductManager.PRODUCT_CATEGORY_SQL, cnn))
          {
            // Fill DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
            {
              // Pass true to generate parameters names matching column names
              ResultText = "Insert: " + builder.GetInsertCommand(true).CommandText;
              ResultText += Environment.NewLine;
              ResultText += "Update: " + builder.GetUpdateCommand(true).CommandText;
              ResultText += Environment.NewLine;
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
        // Create SQL connection object in using block for automatic closing and disposing
        using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
        {
          // Create a SQL Data Adapter
          using (SqlDataAdapter da = new SqlDataAdapter(ProductManager.PRODUCT_SQL, cnn))
          {
            // Fill DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);

            using (SqlCommandBuilder builder = new SqlCommandBuilder(da))
            {
              using (SqlCommand cmd = builder.GetInsertCommand(true))
              {
                cmd.Parameters["@ProductName"].Value = "A New Product 123";
                cmd.Parameters["@IntroductionDate"].Value = DateTime.Now;
                cmd.Parameters["@Url"].Value = "www.fairwaytech.com";
                cmd.Parameters["@Price"].Value = 100;
                cmd.Parameters["@RetireDate"].Value = DateTime.Now.AddMonths(3);
                cmd.Parameters["@ProductCategoryId"].Value = 1;

                // Open the connection for inserting
                cnn.Open();

                cmd.Connection = cnn;
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
