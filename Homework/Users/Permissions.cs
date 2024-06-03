namespace ClientWFP.Users
{
    public abstract class Permissions
    {
        protected Permission[] _permissions;

        public bool Has(Permission permission)
        {
            if(_permissions == null)
            {
                return false;
            }

            return _permissions.Contains(permission);
        }
    }
}
