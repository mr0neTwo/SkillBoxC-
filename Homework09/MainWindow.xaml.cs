using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Homework09
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SeparatorPage separatorPage = new SeparatorPage();
        ReverserPage reverserPage = new ReverserPage();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = separatorPage;
        }

        private void OnSeparatorButtonClicked(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = separatorPage;
        }

        private void OnReverserButtonClicked(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = reverserPage;
        }
    }
}