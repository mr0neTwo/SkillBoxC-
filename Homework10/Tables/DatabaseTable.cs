using Database.Exceptions;
using System.Xml.Serialization;
using Utilites;

namespace Database.Tables
{
    public abstract class DatabaseTable<T> where T : IDatabaseTable
    {
        public event Action DataChanged;

        public abstract string DataPath { get; }

        protected List<T> _items = new();
        private int _currentId;

        protected DatabaseTable()
        {
            LoadDataBase();
            LoadCurrentId();
        }

        public virtual void Add(int userId, T item)
        {
            if (CheckUserId(userId))
            {
                throw new InvalidUserIdException();
            }

            item.Id = ++_currentId;
            item.CreatedAt = Utils.DateTimeToUnixTimestamp(DateTime.Now);
            item.CreatedBy = userId;
            _items.Add(item);

            SaveDatabase();
        }

        public void Remove(T item)
        {
            _items.Remove(item);

            SaveDatabase();
        }

        public T[] GetAll()
        {
            return _items.ToArray();
        }


        public virtual void Update(int userId, T item)
        {
            if (CheckUserId(userId))
            {
                throw new InvalidUserIdException();
            }

            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Id == item.Id)
                {
                    item.UpdatedAt = Utils.DateTimeToUnixTimestamp(DateTime.Now);
                    item.UpdatedBy = userId;
                    _items[i] = item;
                }
            }

            SaveDatabase();
        }

        private void LoadDataBase()
        {
            if (!File.Exists(DataPath))
            {
                return;
            }

            XmlSerializer xmlSerializer = new(typeof(List<T>));
            Stream fStream = new FileStream(DataPath, FileMode.Open, FileAccess.Read);

            if (xmlSerializer.Deserialize(fStream) is List<T> item)
            {
                _items = item;
            }

            fStream.Close();
        }

        private void SaveDatabase()
        {
            XmlSerializer xmlSerializer = new(typeof(List<T>));
            Stream fStream = new FileStream(DataPath, FileMode.Create, FileAccess.Write);
            xmlSerializer.Serialize(fStream, _items);
            fStream.Close();

            DataChanged?.Invoke();
        }

        private void LoadCurrentId()
        {
            if (_items.Count == 0)
            {
                _currentId = 0;

                return;
            }

            _currentId = _items.Select(item => item.Id).Max();
        }

        private static bool CheckUserId(int userId)
        {
            return userId <= 0;
        }
    }
}
