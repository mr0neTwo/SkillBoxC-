﻿namespace Database
{
    public interface IDatabaseTable
    {
        int Id { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}
