using ADONET_Samples.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ADONET_Samples.UserControls
{
  public partial class ConnectionControl : UserControl
  {
    public ConnectionControl()
    {
      InitializeComponent();

      _viewModel = (ConnectionViewModel)this.Resources["viewModel"];
    }

    private readonly ConnectionViewModel _viewModel;

    private void OpenConnectionWindowsAuth_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.Connect(_viewModel.ConnectionString);
    }

    private void OpenConnectionSQLServerAuth_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.Connect("Server=Localhost;Database=ADONETSamples;User ID=sa;Password=P@ssw0rd;");
    }

    private void ConnectionUsingBlock_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.ConnectUsingBlock();
    }

    private void ConnectionWithErrors_Click(object sender, RoutedEventArgs e)
    {
      _viewModel.ConnectWithErrors();
    }

    private void ConnectionStringsDotCom_Click(object sender, RoutedEventArgs e)
    {
      System.Diagnostics.Process.Start("https://www.connectionstrings.com");
    }
  }
}
