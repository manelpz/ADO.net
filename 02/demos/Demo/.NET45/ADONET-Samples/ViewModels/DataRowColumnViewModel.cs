using ADONET_Samples.ManagerClasses;
using System.Data;

namespace ADONET_Samples.ViewModels
{
  public class DataRowColumnViewModel : ViewModelBase
  {
    #region BuildDataTable Method
    public DataTable BuildDataTable()
    {
      DataTable dt = new DataTable();
      DataColumn dc;

      // Use Add() method to build column
      dt.Columns.Add("ProductId", typeof(int));
      // Create a DataColumn object for more control
      dc = new DataColumn
      {
        DataType = typeof(string),
        ColumnName = "ProductName",
        Caption = "Product Name",
        ReadOnly = false
      };
      dt.Columns.Add(dc);
      // Create the DataColumn object in Add() method
      dt.Columns.Add(new DataColumn
      {
        DataType = typeof(decimal),
        ColumnName = "Price",
        Caption = "Price",
        ReadOnly = false
      });

      // Pass in variable amount of arguments to Add() method
      // Pass data in the same order as the columns you created
      dt.Rows.Add(1, "VB.NET Fundamentals", 9.99D);

      // Create a DataRow
      DataRow dr = dt.NewRow();
      dr["ProductId"] = 2;
      dr["ProductName"] = "XML Fundamentals in C#";
      dr["Price"] = 19D;
      dt.Rows.Add(dr);

      // After you are done adding all rows, call AcceptChanges
      dt.AcceptChanges();

      return dt;
    }
    #endregion

    #region CloneDataTable Method
    public DataTable CloneDataTable()
    {
      DataTable dt = BuildDataTable();
      // Clone just the structure of the DataTable
      DataTable dtNew = dt.Clone();

      return dtNew;
    }
    #endregion

    #region CopyDataTable Method
    public DataTable CopyDataTable()
    {
      DataTable dt = BuildDataTable();
      // Clone the structure and copy all data
      DataTable dtNew = dt.Copy();      

      return dtNew;
    }
    #endregion

    #region SelectCopyRowByRow Method
    public DataTable SelectCopyRowByRow()
    {
      DataTable dt;
      DataTable dtNew;
      ProductManager mgr = new ProductManager();

      // Get Products from Table
      dt = mgr.GetProductsAsDataTable();

      // Clone structure into new DataTable
      dtNew = dt.Clone();

      // Select Rows from DataTableObject
      DataRow[] rows = dt.Select("Price < 20");

      // Loop through array of rows
      foreach (DataRow row in rows)
      {
        // NOTE: The following causes an error that a single row can not belong to more than one data table
        // dtNew.Rows.Add(row); 

        // Use ItemArray to avoid the error
        //dtNew.Rows.Add(row.ItemArray);

        // Use ImportRow to avoid error
        dtNew.ImportRow(row);
      }
      dtNew.AcceptChanges();
      
      return dtNew;
    }
    #endregion

    #region SelectUsingCopyToDataTable Method
    public DataTable SelectUsingCopyToDataTable()
    {
      DataTable dt;
      DataTable dtNew;
      ProductManager mgr = new ProductManager();

      // Get Products from Table
      dt = mgr.GetProductsAsDataTable();

      // Select Rows from DataTableObject and Copy to New DataTable
      dtNew = dt.Select("Price < 20").CopyToDataTable();
                  
      return dtNew;
    }
    #endregion
  }
}
