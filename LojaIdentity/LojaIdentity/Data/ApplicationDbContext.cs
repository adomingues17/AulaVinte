using LojaIdentity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LojaIdentity.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //
        modelBuilder.Entity<Produto>().ToTable("Produtos");

        modelBuilder.Entity<Produto>().Property(v => v.Preco).HasPrecision(10, 2);
    }
}
