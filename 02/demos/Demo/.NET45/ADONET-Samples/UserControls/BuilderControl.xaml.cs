using ADONET_Samples.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ADONET_Samples.UserControls
{
  public partial class BuilderControl : UserControl
  {
    public BuilderControl()
    {
      InitializeComponent();

      _viewModel = (BuilderViewModel)this.Resources["viewModel"];
    }

    private readonly BuilderViewModel _viewModel;

    private void BreakApartConnectionString_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.BreakApartConnectionString();
    }

    private void CreateConnectionString_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.CreateConnectionString();
    }

    private void CreateDataModificationCommands_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.CreateDataModificationCommands();
    }

    private void InsertUsingDataModificationCommand_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.InsertUsingDataModificationCommand();
    }
  }
}
