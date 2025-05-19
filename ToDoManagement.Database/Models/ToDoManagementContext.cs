using Microsoft.EntityFrameworkCore;

namespace ToDoManagement.Database.Models;

public class ToDoManagementContext : DbContext
{
    public DbSet<ToDoType> ToDoTypes { get; set; }
    //public DbSet<ToDoDetail> ToDoDetails { get; set; }
    //public DbSet<ToDoItem> ToDoItems { get; set; }

    public ToDoManagementContext(DbContextOptions<ToDoManagementContext> options)
       : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure ToDoType
        modelBuilder.Entity<ToDoType>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(200);

            entity.Property(e => e.Description)
                .HasMaxLength(200);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200);

            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(200);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAddOrUpdate();
        });

        base.OnModelCreating(modelBuilder);
    }
}
