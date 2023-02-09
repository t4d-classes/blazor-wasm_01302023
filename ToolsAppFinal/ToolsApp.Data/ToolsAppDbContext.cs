using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using ToolsApp.Data.Models;
using System.Reflection.Metadata;

namespace ToolsApp.Data
{
  public class ToolsAppDbContext : IdentityDbContext
  {
    public ToolsAppDbContext(DbContextOptions<ToolsAppDbContext> options) : base(options) { }

    public DbSet<Color> Colors { get; set; }
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Car>()
          .Property(c => c.Price)
          .HasColumnType("decimal")
          .HasPrecision(18,2)
          .IsRequired();
    }
  }
}