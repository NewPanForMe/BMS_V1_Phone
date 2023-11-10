using BMS_V2_Db.Models;
using Microsoft.EntityFrameworkCore;

namespace BMS_V2_Db;
public class BmsV2DbContext : DbContext
{
    /// <summary>
    /// 用户
    /// </summary>
    public DbSet<User> User { get; set; } = null!;

    public BmsV2DbContext(DbContextOptions<BmsV2DbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
