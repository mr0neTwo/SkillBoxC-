namespace Database
{
    public struct Client : IDatabaseTable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ThirdName { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportNumber { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

    }
}
