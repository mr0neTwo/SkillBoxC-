namespace ClientWFP.Users
{
    internal class ConsultantPermissions : Permissions
    {
        public ConsultantPermissions()
        {
            _permissions = new[]
            {
                Permission.CanReadClients,
                Permission.CanEditClient,
                Permission.CanEditClientPhone
            };
        }
        
    }
}
