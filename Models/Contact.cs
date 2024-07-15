using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ZadanieRekrutacyjne.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public string? Phone { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
