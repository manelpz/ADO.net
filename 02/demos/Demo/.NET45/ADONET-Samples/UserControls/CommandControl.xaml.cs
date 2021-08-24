using ADONET_Samples.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ADONET_Samples.UserControls
{
  public partial class CommandControl : UserControl
  {
    public CommandControl()
    {
      InitializeComponent();

      _viewModel = (CommandViewModel)this.Resources["viewModel"];
    }

    private readonly CommandViewModel _viewModel;

    private void Insert_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.InsertProduct();
    }

    private void Scalar_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.GetProductsCountScalar();
    }

    private void ScalarParameters_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.GetProductsCountScalarUsingParameters();
    }

    private void InsertParameters_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.InsertProductUsingParameters();
    }

    private void OutputParameters_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.InsertProductOutputParameters();
    }

    private void Transaction_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.TransactionProcessing();
    }
  }
}
