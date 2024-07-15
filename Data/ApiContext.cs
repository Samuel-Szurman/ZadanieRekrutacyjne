using Microsoft.EntityFrameworkCore;
using ZadanieRekrutacyjne.Models;

namespace ZadanieRekrutacyjne.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
