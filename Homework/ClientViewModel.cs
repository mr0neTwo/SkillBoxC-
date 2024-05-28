using Database;
using System.Collections.ObjectModel;

namespace ClientWFP
{
    public class ClientViewModel
    {
        public ObservableCollection<Client> Clients { get; set; }

        public ClientViewModel()
        {
            Clients = new ObservableCollection<Client>();
        }

        public void UpdateClients(IEnumerable<Client> clients)
        {
            Clients.Clear();

            foreach (var client in clients)
            {

                Clients.Add(client);
            }
        }
    }

}