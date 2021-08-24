using ADONET_Samples.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ADONET_Samples.UserControls
{
  public partial class ExceptionControl : UserControl
  {
    public ExceptionControl()
    {
      InitializeComponent();

      _viewModel = (ExceptionViewModel)this.Resources["viewModel"];
    }

    private readonly ExceptionViewModel _viewModel;

    private void SimpleExceptionHandling_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.SimpleExceptionHandling();
    }

    private void CatchSqlException_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.CatchSqlException();
    }

    private void GatherExceptionInfo_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.GatherExceptionInformation();
    }
  }
}
