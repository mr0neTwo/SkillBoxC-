using Database;

namespace ClientWFP.Users
{
    public class Manager : User
    {
        public override Permissions Permissions => _permissions;

        private Permissions _permissions;

        public Manager(int id, string name) : base(id, name)
        {
            _permissions = new ManagerPermissions();
        }
    }
}
