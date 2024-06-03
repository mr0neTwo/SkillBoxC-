using ClientWFP.Users;
using Database;
using System.Windows;

namespace ClientWFP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataProvider _dataProvider;


        private Clients _clientsPage;
        private SettingPage _settingPage;


        public MainWindow()
        {

            AppData _appData = new AppData();

            _clientsPage = new Clients(_appData);
            _settingPage = new SettingPage(_appData);

            InitializeComponent();

            MainFrame.Content = _clientsPage;
        }


        private void OnClientsButtonClicked(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _clientsPage;
        }

        private void OnSettingsButtonClicked(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _settingPage;
        }
    }
}