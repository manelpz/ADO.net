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

    private void DataReaderMenu_Click(object sender, RoutedEventArgs e)
    {
      contentArea.Children.Clear();
      contentArea.Children.Add(new DataReaderControl());
    }

    private void ExceptionHandlingMenu_Click(object sender, RoutedEventArgs e)
    {
      contentArea.Children.Clear();
      contentArea.Children.Add(new ExceptionControl());
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

    private void DataRowColumnMenu_Click(object sender, RoutedEventArgs e)
    {
      contentArea.Children.Clear();
      contentArea.Children.Add(new DataRowColumnControl());
    }

    private void BuilderClassesMenu_Click(object sender, RoutedEventArgs e)
    {
      contentArea.Children.Clear();
      contentArea.Children.Add(new BuilderControl());
    }
  }
}
