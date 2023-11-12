using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace mvc1.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}
        public DbSet<Produto> Produtos { get; set; }
    }
}