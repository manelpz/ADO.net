using System;
using System.Data;
using System.Data.SqlClient;

namespace ADONET_Samples.ViewModels
{
  public class CommandViewModel : ViewModelBase
  {
    #region Private Variables
    private Product _InputEntity = new Product { ProductId = -1, ProductName = "A New Product", IntroductionDate = DateTime.Now, Price = 29, RetireDate = null, Url = "www.fairwaytech.com" };
    private Product _SearchEntity = new Product { ProductName = "WPF%" };
    #endregion

    #region Public Properties
    /// <summary>
    /// Get/Set Entity for Data Entry
    /// </summary>
    public Product InputEntity
    {
      get { return _InputEntity; }
      set {
        _InputEntity = value;
        RaisePropertyChanged("InputEntity");
      }
    }

    /// <summary>
    /// Get/Set Entity for Searching
    /// </summary>
    public Product SearchEntity
    {
      get { return _SearchEntity; }
      set {
        _SearchEntity = value;
        RaisePropertyChanged("SearchEntity");
      }
    }
    #endregion

    #region GetProductsCountScalar Method
    public int GetProductsCountScalar()
    {
      RowsAffected = 0;

      // Create SQL statement to submit
      string sql = "SELECT COUNT(*) FROM Product";

      // Create a connection
      using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
      {
        // Open the connection
        cnn.Open();

        // Create command object
        using (SqlCommand cmd = new SqlCommand(sql, cnn))
        {
          RowsAffected = (int)cmd.ExecuteScalar();
        }
      }

      ResultText = "Rows Affected: " + RowsAffected.ToString();

      return RowsAffected;
    }
    #endregion

    #region GetProductsCountScalarUsingParameters Method
    public int GetProductsCountScalarUsingParameters()
    {
      RowsAffected = 0;

      // Create SQL statement to submit
      string sql = "SELECT COUNT(*) FROM Product";
      sql += " WHERE ProductName LIKE @ProductName";

      // Create a connection
      using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
      {
        // Open the connection
        cnn.Open();

        // Create command object
        using (SqlCommand cmd = new SqlCommand(sql, cnn))
        {
          // Create parameters
          cmd.Parameters.Add(new SqlParameter("@ProductName", SearchEntity.ProductName));

          RowsAffected = (int)cmd.ExecuteScalar();
        }
      }

      ResultText = "Rows Affected: " + RowsAffected.ToString();

      return RowsAffected;
    }
    #endregion

    #region InsertProduct Method
    public int InsertProduct()
    {
      RowsAffected = 0;

      // Create SQL statement to submit
      string sql = "INSERT INTO Product(ProductName, IntroductionDate, Url, Price)";
      sql += " VALUES('VB.NET Fundamentals', '2019-05-21', 'https://bit.ly/30KKHjs', 19.99)";

      try
      {
        // Create SQL connection object in using block for automatic closing and disposing
        using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
        {
          // Open the connection
          cnn.Open();

          // Create command object in using block for automatic disposal
          using (SqlCommand cmd = new SqlCommand(sql, cnn))
          {
            // Set CommandType
            cmd.CommandType = CommandType.Text;
            // Execute the INSERT statement
            RowsAffected = cmd.ExecuteNonQuery();

            ResultText = "Rows Affected: " + RowsAffected.ToString();
          }
        }
      }
      catch (Exception ex)
      {
        ResultText = ex.ToString();
      }

      return RowsAffected;
    }
    #endregion

    #region InsertProductUsingParameters Method
    public int InsertProductUsingParameters()
    {
      RowsAffected = 0;

      // Create SQL statement to submit
      string sql = "INSERT INTO Product(ProductName, IntroductionDate, Url, Price)";
      sql += " VALUES(@ProductName, @IntroductionDate, @Url, @Price)";

      try
      {
        // Create SQL connection object in using block for automatic closing and disposing
        using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
        {
          // Open the connection
          cnn.Open();

          // Create command object in using block for automatic disposal
          using (SqlCommand cmd = new SqlCommand(sql, cnn))
          {
            // Create input parameters
            cmd.Parameters.Add(new SqlParameter("@ProductName", InputEntity.ProductName));
            cmd.Parameters.Add(new SqlParameter("@IntroductionDate", InputEntity.IntroductionDate));
            cmd.Parameters.Add(new SqlParameter("@Url", InputEntity.Url));
            cmd.Parameters.Add(new SqlParameter("@Price", InputEntity.Price));

            // Set CommandType
            cmd.CommandType = CommandType.Text;
            // Execute the INSERT statement
            RowsAffected = cmd.ExecuteNonQuery();

            ResultText = "Rows Affected: " + RowsAffected.ToString();
          }
        }
      }
      catch (Exception ex)
      {
        ResultText = ex.ToString();
      }

      return RowsAffected;
    }
    #endregion

    #region InsertProductOutputParameters Method
    public int InsertProductOutputParameters()
    {
      RowsAffected = 0;

      // Create SQL statement to submit
      string sql = "Product_Insert";

      try
      {
        // Create SQL connection object in using block for automatic closing and disposing
        using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
        {
          // Open the connection
          cnn.Open();

          // Create command object in using block for automatic disposal
          using (SqlCommand cmd = new SqlCommand(sql, cnn))
          {
            // Create input parameters
            cmd.Parameters.Add(new SqlParameter("@ProductName", InputEntity.ProductName));
            cmd.Parameters.Add(new SqlParameter("@IntroductionDate", InputEntity.IntroductionDate));
            cmd.Parameters.Add(new SqlParameter("@Url", InputEntity.Url));
            cmd.Parameters.Add(new SqlParameter("@Price", InputEntity.Price));

            // Create OUTPUT parameter
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@ProductId", Value = InputEntity.ProductId, IsNullable = false, DbType = System.Data.DbType.Int32, Direction = ParameterDirection.Output });

            // Set CommandType to Stored Procedure
            cmd.CommandType = CommandType.StoredProcedure;
            // Execute the INSERT statement
            RowsAffected = cmd.ExecuteNonQuery();

            // Get output parameter
            InputEntity.ProductId = (int)cmd.Parameters["@ProductId"].Value;
            RaisePropertyChanged("InputEntity");

            ResultText = "Rows Affected: " + RowsAffected.ToString();
          }
        }
      }
      catch (Exception ex)
      {
        ResultText = ex.ToString();
      }

      return RowsAffected;
    }
    #endregion

    #region TransactionProcessing Method
    public int TransactionProcessing()
    {
      RowsAffected = 0;

      // Create SQL statement to submit
      string sql = "INSERT INTO Product(ProductName, IntroductionDate, Url, Price)";
      sql += " VALUES(@ProductName, @IntroductionDate, @Url, @Price)";

      try
      {
        // Create SQL connection object in using block for automatic closing and disposing
        using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString))
        {
          // Open the connection
          cnn.Open();

          // Start a local transaction
          using (SqlTransaction trn = cnn.BeginTransaction())
          {
            try
            {
              // Create command object in using block for automatic disposal
              using (SqlCommand cmd = new SqlCommand(sql, cnn))
              {
                // Make command object part of the transaction
                cmd.Transaction = trn;

                // Create input parameters
                cmd.Parameters.Add(new SqlParameter("@ProductName", InputEntity.ProductName));
                cmd.Parameters.Add(new SqlParameter("@IntroductionDate", InputEntity.IntroductionDate));
                cmd.Parameters.Add(new SqlParameter("@Url", InputEntity.Url));
                cmd.Parameters.Add(new SqlParameter("@Price", InputEntity.Price));

                // Set CommandType
                cmd.CommandType = CommandType.Text;
                // Execute the INSERT statement
                RowsAffected = cmd.ExecuteNonQuery();

                ResultText = "Product Rows Affected: " + RowsAffected.ToString() + Environment.NewLine;

                // *************************************************
                // ** SECOND STATEMENT TO EXECUTE
                // *************************************************
                sql = "INSERT INTO ProductCategory(CategoryName)";
                sql += " VALUES(@CategoryName)";

                // Reset the command text
                cmd.CommandText = sql;
                // Create previous parameters
                cmd.Parameters.Clear();
                // Create input parameter
                cmd.Parameters.Add(new SqlParameter("@CategoryName", "A New Category"));

                // Execute the INSERT statement
                RowsAffected = cmd.ExecuteNonQuery();

                ResultText += "Product Category Rows Affected: " + RowsAffected.ToString();

                // Finish the Transaction
                trn.Commit();
              }
            }
            catch (Exception ex)  // Catch block for transaction
            {
              // Rollback the transaction
              trn.Rollback();

              ResultText = "Transaction Rolled Back" + Environment.NewLine + ex.ToString();
            }
          }
        }
      }
      catch (Exception ex)  // Catch block for connection opening
      {
        ResultText = ex.ToString();
      }

      return RowsAffected;
    }
    #endregion
  }
}
