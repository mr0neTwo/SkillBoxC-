namespace Database
{
    public class Clients : DatabaseTable<Client>
    {
        public override string DataPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../clients.xml");

        public Client[] Find(string searchWord)
        {
            List<Client> result = new List<Client>();

            searchWord = searchWord.ToLower();

            foreach (Client client in _items)
            {
                if (client.FirstName != null && client.FirstName.ToLower().Contains(searchWord))
                {
                    result.Add(client);
                }
                else if (client.LastName != null && client.LastName.ToLower().Contains(searchWord))
                {
                    result.Add(client);
                }
                else if (client.ThirdName != null && client.ThirdName.ToLower().Contains(searchWord))
                {
                    result.Add(client);
                }
                else if (client.PhoneNumber != null && client.PhoneNumber.ToLower().Contains(searchWord))
                {
                    result.Add(client);
                }
            }

            return result.ToArray();
        }
    }
}
