using ClientWFP.Users;

namespace ClientWFP
{
    public class AppData
    {
        public event Action UserChanged;
        public DataProvider DataProvider => _dataProvider;
        public User CurrentUser => _currentUser;

        private DataProvider _dataProvider;
        private User _currentUser;

        public AppData()
        {
            User defaultUser = new Consultant(0, "Smith");
            _currentUser = defaultUser;
            _dataProvider = new DataProvider(defaultUser);
        }

        public void ChangeUser(User user)
        {
            _currentUser = user;

            _dataProvider.ChangeUser(user);
            UserChanged?.Invoke();
        }
    }

}