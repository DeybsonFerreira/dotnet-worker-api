using dotnet_worker.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_worker;

public partial class MyAppContext : DbContext
{
    private IConfiguration _configuration { get; }

    public MyAppContext(DbContextOptions<MyAppContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Messages> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DatabaseConnection"));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Messages>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Messages__3214EC070B1C7B67");
            entity.Property(e => e.Read);

            entity.Property(e => e.Text)
                .HasMaxLength(300)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
