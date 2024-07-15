using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ZadanieRekrutacyjne.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
