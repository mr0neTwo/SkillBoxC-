namespace ClientWFP.Users
{
    public abstract class User
    {
        public int Id { get; }
        public string Name { get; }
        public abstract Permissions Permissions { get; }

        protected User(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
