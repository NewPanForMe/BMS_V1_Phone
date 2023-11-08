using Microsoft.EntityFrameworkCore;

namespace BMS_V2_Db
{
    public class BmsV2DbContext:DbContext
    {
        public BmsV2DbContext(DbContextOptions<BmsV2DbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
