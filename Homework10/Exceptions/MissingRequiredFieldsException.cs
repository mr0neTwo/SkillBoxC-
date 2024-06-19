using Database.Extension;
using System.Data;

namespace Database.Exceptions
{
    public class MissingRequiredFieldsException : DataException
    {
        public MissingRequiredFieldsException(): base("One or more required fields are missing.")
        {
        }

        public MissingRequiredFieldsException(params string[] missingFields) : base($"Missing fields: {missingFields.ToBeautifulString()}")
        {
        }
    }
}
