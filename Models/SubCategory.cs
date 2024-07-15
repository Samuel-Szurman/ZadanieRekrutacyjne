using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ZadanieRekrutacyjne.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
