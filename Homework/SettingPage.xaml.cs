using ClientWFP.Users;
using System.Windows.Controls;

namespace ClientWFP
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Page
    {
        private readonly Manager _manager;
        private readonly Consultant _consultant;
        private readonly AppData _appData;

        public SettingPage(AppData appData)
        {
            InitializeComponent();

            _appData = appData;
            _manager = new Manager(1, "Bob");
            _consultant = new Consultant(2, "Greg");

            UserTypeComboBox.ItemsSource = Enum.GetValues(typeof(Role));
            UserTypeComboBox.SelectedValue = Role.Consultant;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(UserTypeComboBox.SelectedItem != null)
            {
                Role selectedRole = (Role)UserTypeComboBox.SelectedItem;

                switch (selectedRole)
                {
                    case Role.Manager:
                        _appData.ChangeUser(_manager);
                        break;
                    case Role.Consultant:
                        _appData.ChangeUser(_consultant);
                        break;
                }
            }
        }
    }
}
