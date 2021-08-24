using ADONET_Samples.ManagerClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ADONET_Samples.ViewModels
{
  public class DataTableViewModel : ViewModelBase
  {
    #region Private Variables
    private object _Products;
    private object _Categories;
    private Product _InputEntity = new Product();
    private Product _SearchEntity = new Product { ProductName = "WPF%" };
    #endregion

    #region Public Properties
    /// <summary>
    /// Get/Set Products collection
    /// </summary>
    public object Products
    {
      get { return _Products; }
      set {
        _Products = value;
        RaisePropertyChanged("Products");
      }
    }

    /// <summary>
    /// Get/Set Category collection
    /// </summary>
    public object Categories
    {
      get { return _Categories; }
      set {
        _Categories = value;
        RaisePropertyChanged("Categories");
      }
    }

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

    #region GetProductsAsDataTable Method
    public DataTable GetProductsAsDataTable()
    {
      // Initialize DataTable object to null in case of an error
      DataTable dt = null;

      // Create a connection
      using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString)) {
        // Open the connection
        cnn.Open();

        // Create command object
        using (SqlCommand cmd = new SqlCommand(ProductManager.PRODUCT_SQL, cnn)) {
          // Create a SQL Data Adapter
          using (SqlDataAdapter da = new SqlDataAdapter(cmd)) {
            // Fill DataTable using Data Adapter
            dt = new DataTable();
            da.Fill(dt);

            // Loop through all rows/columns
            ProcessRowsAndColumns(dt);
          }
        }
      }

      return dt;
    }
    #endregion

    #region ProcessRowsAndColumns Method
    private void ProcessRowsAndColumns(DataTable dt)
    {
      StringBuilder sb = new StringBuilder(2048);
      int index = 1;

      // Process each row
      foreach (DataRow row in dt.Rows) {
        sb.AppendLine("** Row: " + index.ToString() + " **");
        // Process each column
        foreach (DataColumn col in dt.Columns) {
          sb.AppendLine(col.ColumnName + ": " + row[col.ColumnName].ToString());
        }
        sb.AppendLine();

        index++;
      }

      ResultText = sb.ToString();
    }
    #endregion

    #region GetProductsAsGenericList Method
    public List<Product> GetProductsAsGenericList()
    {
      List<Product> ret = new List<Product>();
      ResultText = string.Empty;
      RowsAffected = 0;

      // Initialize DataTable object to null in case of an error
      DataTable dt = null;

      // Create SQL statement to submit
      string sql = ProductManager.PRODUCT_SQL;
      sql += " WHERE ProductName LIKE @ProductName";

      // Create a connection
      using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString)) {
        // Create command object
        using (SqlCommand cmd = new SqlCommand(sql, cnn)) {
          // Create parameter
          cmd.Parameters.Add(new SqlParameter("@ProductName", SearchEntity.ProductName));

          // Create a SQL Data Adapter
          using (SqlDataAdapter da = new SqlDataAdapter(cmd)) {
            // Fill DataTable using Data Adapter
            dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0) {
              ret =
                (from row in dt.AsEnumerable()
                 select new Product
                 {                   
                   ProductId = row.Field<int>("ProductId"),
                   ProductName = row.Field<string>("ProductName"),
                   IntroductionDate = row.Field<DateTime>("IntroductionDate"),
                   Url = row.Field<string>("Url"),
                   Price = row.Field<decimal>("Price"),
                   RetireDate = row.Field<DateTime?>("RetireDate"),
                   ProductCategoryId = row.Field<int?>("ProductCategoryId")
                 }).ToList();
            }
          }
        }
      }

      return ret;
    }
    #endregion

    #region GetMultipleResultSets Method
    public List<Product> GetMultipleResultSets()
    {
      List<Product> ret = new List<Product>();
      ResultText = string.Empty;
      RowsAffected = 0;
      DataSet ds = new DataSet();

      // Create SQL statement to submit
      string sql = ProductManager.PRODUCT_SQL;
      sql += ";" + ProductManager.PRODUCT_CATEGORY_SQL;

      // Create a connection
      using (SqlConnection cnn = new SqlConnection(AppSettings.ConnectionString)) {
        // Create command object
        using (SqlCommand cmd = new SqlCommand(sql, cnn)) {
          // Create a SQL Data Adapter
          using (SqlDataAdapter da = new SqlDataAdapter(cmd)) {
            // Fill DataSet using Data Adapter            
            da.Fill(ds);

            if (ds.Tables.Count > 0) {
              Products = ds.Tables[0];
              Categories = ds.Tables[1];
            }
          }
        }
      }

      return ret;
    }
    #endregion
  }
}
