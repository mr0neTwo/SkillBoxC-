using ClientWFP;
using Database;
using System.Windows;

namespace ClientWFP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataBase _dataBase;
        private Clients _clientsPage;
        private SettingPage _settingPage;

        private Manager _manager;
        private Consultant _consultant;

        public MainWindow()
        {
            _dataBase = new DataBase();
            _clientsPage = new Clients(_dataBase);
            _settingPage = new SettingPage();

            _manager = new Manager(1, "Mark");
            _consultant = new Consultant(2, "Bob");

            InitializeComponent();

            MainFrame.Content = _clientsPage;

            _settingPage.RoleSelected += OnRoleSelected;
            _clientsPage.ChangeUser(_consultant);
        }


        private void OnClientsButtonClicked(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _clientsPage;
        }

        private void OnSettingsButtonClicked(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = _settingPage;
        }

        private void OnRoleSelected(Role role)
        {
            switch (role)
            {
                case Role.Manager:
                    _clientsPage.ChangeUser(_manager);
                    break;
                case Role.Consultant:
                    _clientsPage.ChangeUser(_consultant);
                    break;
            }
        }
    }

}