namespace ClientWFP.Users
{
    internal class ManagerPermissions : Permissions
    {
        public ManagerPermissions()
        {
            _permissions = new[]
            {
                Permission.CanReadClients,
                Permission.CanEditClientName,
                Permission.CanEditClientPhone,
                Permission.CanEditClientPasswordNumber,
                Permission.CanRemoveClient,
                Permission.CanRemoveClient,
                Permission.CanAddClient,
                Permission.CanReadPasswordData,
                Permission.CanEditClient
            };
        }

    }
}
