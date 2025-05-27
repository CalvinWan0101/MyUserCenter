using Microsoft.EntityFrameworkCore;
using MyUserCenter.Domain;

namespace MyUserCenter.EFCore;

public class MyUserCenterDbContext : DbContext {
    public MyUserCenterDbContext(DbContextOptions<MyUserCenterDbContext> options) : base(options) { }

    public DbSet<MyUser> MyUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MyUser>().ToTable("MyUsers");
    }
}
