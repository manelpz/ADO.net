using ADONET_Samples.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ADONET_Samples.UserControls
{
  public partial class DataViewControl : UserControl
  {
    public DataViewControl()
    {
      InitializeComponent();

      _viewModel = (DataViewViewModel)this.Resources["viewModel"];
    }

    private readonly DataViewViewModel _viewModel;

    private void DataViewPriceDesc_Click(object sender, RoutedEventArgs e)
    {
      grdProducts.DataContext = _viewModel.GetProductsSortedByPriceDescending();
    }

    private void DataViewPriceLessThan_Click(object sender, RoutedEventArgs e)
    {
      grdProducts.DataContext = _viewModel.GetProductsPriceLessThan(20);
    }

    private void DataViewUsingLINQ_Click(object sender, RoutedEventArgs e)
    {
      grdProducts.DataContext = _viewModel.GetProductsPriceLessThanLINQ(20);
    }

    private void DataViewToDataTable_Click(object sender, RoutedEventArgs e)
    {
      grdProducts.DataContext = _viewModel.DataViewToDataTable();
    }
  }
}
