using ADONET_Samples.UserControls;
using System.Windows;

namespace ADONET_Samples
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void ExitMenu_Click(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    private void ConnectionsMenu_Click(object sender, RoutedEventArgs e)
    {
      contentArea.Children.Clear();
      contentArea.Children.Add(new ConnectionControl());
    }

    private void CommandsMenu_Click(object sender, RoutedEventArgs e)
    {
      contentArea.Children.Clear();
      contentArea.Children.Add(new CommandControl());
    }
  }
}
