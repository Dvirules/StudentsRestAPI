using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using StudentsRestAPI.Models;

namespace StudentsRestAPI.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;
    }
}