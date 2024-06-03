using Database;
using System.Text.RegularExpressions;

namespace ClientWFP.Users
{
    public class Consultant : User
    {
        public override Permissions Permissions => _permissions;

        public Permissions _permissions;

        public Consultant(int id, string name) : base(id, name)
        {
            _permissions = new ConsultantPermissions();
        }
    }
}
