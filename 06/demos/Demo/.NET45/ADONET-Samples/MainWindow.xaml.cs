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

    private void DataTableMenu_Click(object sender, RoutedEventArgs e)
    {
      contentArea.Children.Clear();
      contentArea.Children.Add(new DataTableControl());
    }

    private void DataViewMenu_Click(object sender, RoutedEventArgs e)
    {
      contentArea.Children.Clear();
      contentArea.Children.Add(new DataViewControl());
    }
  }
}
