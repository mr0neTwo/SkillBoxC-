namespace Database.Extension
{
    public static class Extension
    {
        public static string ToBeautifulString<T>(this IEnumerable<T> array)
        {
            // IEnumerable<string> stringArray = array.Where(element => element != null).Select(element => element.ToString());

            return $"[{string.Join(", ", array)}]";
        }
    }
}
