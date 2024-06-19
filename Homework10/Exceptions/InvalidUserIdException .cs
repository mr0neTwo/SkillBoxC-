using System.Data;

namespace Database.Exceptions
{
    public class InvalidUserIdException : DataException
    {
        public InvalidUserIdException(): base("The userId is invalid.")
        {
        }

        public InvalidUserIdException(int userId): base($"The userId: \"{userId}\" is invalid.")
        {
        }
    }
}

