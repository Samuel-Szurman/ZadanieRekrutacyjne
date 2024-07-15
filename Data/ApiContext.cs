using Microsoft.EntityFrameworkCore;
using ZadanieRekrutacyjne.Models;

namespace ZadanieRekrutacyjne.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
    }
}
