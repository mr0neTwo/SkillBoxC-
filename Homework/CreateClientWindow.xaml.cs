using ClientWFP.Users;
using Database.DataStruct;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClientWFP
{
    /// <summary>
    /// Interaction logic for CreateClientWindow.xaml
    /// </summary>
    public partial class ClientDataWindow : Window
    {
        public Client Client { get => GetClient(); set => SetClient(value); }

        private Client _client;

        public ClientDataWindow()
        {
            InitializeComponent();
        }

        public void EnableFieldByPermissions(Permissions permissions)
        {
            FirstNameTextBox.IsEnabled = permissions.Has(Permission.CanEditClientName);
            LastNameTextBox.IsEnabled = permissions.Has(Permission.CanEditClientName);
            ThirdNameTextBox.IsEnabled = permissions.Has(Permission.CanEditClientName); ;
            PhoneNumberTextBox.IsEnabled = permissions.Has(Permission.CanEditClientPhone);
            PassportNumberTextBox.IsEnabled = permissions.Has(Permission.CanEditClientPasswordNumber);
        }

        private void SetClient(Client client)
        {
            _client = client;
            FirstNameTextBox.Text = client.FirstName;
            LastNameTextBox.Text = client.LastName;
            ThirdNameTextBox.Text = client.ThirdName;
            PhoneNumberTextBox.Text = client.PhoneNumber;
            PassportNumberTextBox.Text = client.PassportNumber;
        }

        private Client GetClient() 
        {
            _client.FirstName = FirstNameTextBox.Text;
            _client.LastName = LastNameTextBox.Text;
            _client.ThirdName = ThirdNameTextBox.Text;
            _client.PhoneNumber = PhoneNumberTextBox.Text;
            _client.PassportNumber = PassportNumberTextBox.Text;

            return _client;
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                DialogResult = true;
                Close();
            }
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool ValidateForm()
        {
            bool isValid = ValidateTextBox(FirstNameTextBox, FirstNameTextBlock);
            isValid &= ValidateTextBox(LastNameTextBox, LastNameTextBlock);
            isValid &= ValidateTextBox(ThirdNameTextBox, ThirdNameTextBlock);
            isValid &= ValidateTextBox(PassportNumberTextBox, PasswordNumberTextBlock);

            return isValid;
        }

        private bool ValidateTextBox(TextBox text, TextBlock textBlock)
        {
            bool isValid = !string.IsNullOrEmpty(text.Text);
            textBlock.Foreground = isValid ? Brushes.Black : Brushes.Red;
            
            return isValid;
        }
    }
}
