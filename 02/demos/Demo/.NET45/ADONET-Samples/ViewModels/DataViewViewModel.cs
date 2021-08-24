using ADONET_Samples.ManagerClasses;
using System.Data;

namespace ADONET_Samples.ViewModels
{
  public class DataViewViewModel : ViewModelBase
  {
    #region GetProductsSortedByPriceDescending Method
    public DataView GetProductsSortedByPriceDescending()
    {
      ProductManager mgr = new ProductManager();
      DataView dv = null;

      // Get Products as a DataTable
      DataTable dt = mgr.GetProductsAsDataTable();
      if (dt != null)
      {
        // Set DataView
        dv = dt.DefaultView;

        // Sort the data
        dv.Sort = "Price DESC";

        // Set RowsAffected
        RowsAffected = dv.Count;

        // Set ResultText
        ResultText = "Rows: " + RowsAffected.ToString();
      }

      return dv;
    }
    #endregion

    #region GetProductsPriceLessThan Method
    public DataView GetProductsPriceLessThan(decimal lessThan)
    {
      ProductManager mgr = new ProductManager();
      DataView dv = null;

      // Get Products as a DataTable
      DataTable dt = mgr.GetProductsAsDataTable();

      if (dt != null)
      {
        // Set DataView
        dv = dt.DefaultView;

        // Filter the Data
        dv.RowFilter = "Price < " + lessThan.ToString();

        // Can filter using And, Or, True, False, Is, Like, etc.
        //dv.RowFilter = "Price < " + lessThan.ToString() + " AND ProductCategoryId = 1";

        // Set RowsAffected
        RowsAffected = dv.Count;

        // Set ResultText
        ResultText = "Rows: " + RowsAffected.ToString();
      }

      return dv;
    }
    #endregion

    #region GetProductsPriceLessThanLINQ Method
    public DataView GetProductsPriceLessThanLINQ(decimal lessThan)
    {
      ProductManager mgr = new ProductManager();
      DataView dv = null;

      // Get Products as a DataTable
      DataTable dt = mgr.GetProductsAsDataTable();

      if (dt != null) {
        dv = (from prod in dt.AsEnumerable()
          where prod.Field<decimal>("Price") < lessThan
          orderby prod.Field<decimal>("Price")
          select prod).AsDataView();

        // Set RowsAffected
        RowsAffected = dv.Count;

        // Set ResultText
        ResultText = "Rows: " + RowsAffected.ToString();
      }

      return dv;
    }
    #endregion

    #region DataViewToDataTable Method
    public DataTable DataViewToDataTable()
    {
      ProductManager mgr = new ProductManager();
      DataView dv = null;
      
      // Get Products as a DataTable
      DataTable dt = mgr.GetProductsAsDataTable();

      if (dt != null) {
        dv = (from prod in dt.AsEnumerable()
              where prod.Field<decimal>("Price") < 29
              orderby prod.Field<decimal>("Price") descending
              select prod).AsDataView();

        // Set RowsAffected
        RowsAffected = dv.Count;

        // Convert back to DataTble
        dt = dv.ToTable();

        // Set ResultText
        ResultText = "Rows: " + RowsAffected.ToString();
      }

      return dt;
    }
    #endregion
  }
}
