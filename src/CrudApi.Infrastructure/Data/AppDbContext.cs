
using CrudApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos => Set<Produto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>().HasKey(p => p.Id);
        base.OnModelCreating(modelBuilder);
    }
}
