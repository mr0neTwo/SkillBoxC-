namespace Database.DataStruct
{
    public struct Transaction : IDatabaseTable
    {
        public int Id { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public Direction Direction { get; set; }
        public float Sum { get; set; }
        public int AccountID { get; set; }
        public int SourceAccountID { get; set; }
        public int DestinationAccountID { get; set; }
        public string Comment { get; set; }
    }
}
