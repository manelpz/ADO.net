using ADONET_Samples.ViewModels;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ADONET_Samples.UserControls
{
  public partial class DataTableControl : UserControl
  {
    public DataTableControl()
    {
      InitializeComponent();

      _viewModel = (DataTableViewModel)this.Resources["viewModel"];
    }

    private readonly DataTableViewModel _viewModel;

    private void DataTable_Click(object sender, RoutedEventArgs e)
    {
      // DefaultView is a bindable view of the DataTable
      grdProducts.DataContext = _viewModel.GetProductsAsDataTable().DefaultView;
    }

    private void GenericList_Click(object sender, RoutedEventArgs e)
    {
      grdProducts.DataContext = _viewModel.GetProductsAsGenericList();
    }

    private void MultipleResultSets_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.GetMultipleResultSets();

      grdProducts.DataContext = ((DataTable)_viewModel.Products).DefaultView;
      grdCategories.DataContext = ((DataTable)_viewModel.Categories).DefaultView;
    }
  }
}
