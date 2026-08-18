using SQLite;

namespace Quick_Queue.Models
{
    public class Cafe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string ImageName { get; set; } = string.Empty;

        public double ServiceTime { get; set; }

        public bool IsActive { get; set; }
    }
}