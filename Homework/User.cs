namespace ClientWFP
{
    public abstract class User
    {
        public int Id { get; }
        public string Name { get; }

        protected abstract AccessType Access { get; }

        protected User(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
