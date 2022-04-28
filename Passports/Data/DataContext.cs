using Microsoft.EntityFrameworkCore;
using Passports.Models;

namespace Passports.Data
{
    public class DataContext : DbContext
    {
        public DbSet<PassportData> Passports { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
