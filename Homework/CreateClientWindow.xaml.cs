using Database;
using System.Windows;

namespace ClientWFP
{
    /// <summary>
    /// Interaction logic for CreateClientWindow.xaml
    /// </summary>
    public partial class CreateClientWindow : Window
    {
        public Client Client { get => GetClient(); set => SetClient(value); }

        private Client _client;

        public CreateClientWindow()
        {
            InitializeComponent();
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
            DialogResult = true;
            Close();
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
