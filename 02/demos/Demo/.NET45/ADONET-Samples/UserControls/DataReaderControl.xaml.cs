using ADONET_Samples.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ADONET_Samples.UserControls
{
  public partial class DataReaderControl : UserControl
  {
    public DataReaderControl()
    {
      InitializeComponent();

      _viewModel = (DataReaderViewModel)this.Resources["viewModel"];
    }

    private readonly DataReaderViewModel _viewModel;

    private void DataReader_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.GetProductsAsDataReader();
    }

    private void GenericList_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.GetProductsAsGenericList();
    }

    private void UsingFieldValue_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.GetProductsUsingFieldValue();
    }

    private void ExtensionMethods_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.GetProductsUsingExtensionMethods();
    }

    private void MultipleResultSets_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.GetMultipleResultSets();
    }
  }
}
