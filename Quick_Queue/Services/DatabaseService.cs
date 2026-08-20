using SQLite;
using Quick_Queue.Models;

namespace Quick_Queue.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _database;

        private async Task Init()
        {
            if (_database != null)
                return;

            string dbPath = Path.Combine(
                FileSystem.AppDataDirectory,
                "quickqueue.db3");

            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<Cafe>();

            await SeedData();
        }

        private async Task SeedData()
        {
            var count = await _database!.Table<Cafe>().CountAsync();

            if (count > 0)
                return;

            var cafes = new List<Cafe>
            {
                new Cafe
                {
                    Name = "Main Campus Café",
                    Location = "Main Campus",
                    ImageName = "cafeone.png",
                    ServiceTime = 2.0,
                    IsActive = true
                },

                new Cafe
                {
                    Name = "Library Café",
                    Location = "Campus Library",
                    ImageName = "cafetwo.png",
                    ServiceTime = 3.0,
                    IsActive = true
                },

                new Cafe
                {
                    Name = "Student Hub Café",
                    Location = "Student Hub",
                    ImageName = "cafethree.png",
                    ServiceTime = 1.5,
                    IsActive = true
                }
            };

            await _database.InsertAllAsync(cafes);
        }

        public async Task<List<Cafe>> GetCafesAsync()
        {
            await Init();

            return await _database!
                .Table<Cafe>()
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<Cafe?> GetCafeAsync(int id)
        {
            await Init();

            return await _database!
                .Table<Cafe>()
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveCafeAsync(Cafe cafe)
        {
            await Init();

            if (cafe.Id != 0)
                return await _database!.UpdateAsync(cafe);

            return await _database!.InsertAsync(cafe);
        }

        public async Task<int> DeleteCafeAsync(Cafe cafe)
        {
            await Init();

            return await _database!.DeleteAsync(cafe);
        }
    }
}