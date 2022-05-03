using Kimed.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Kimed.Data.Context
{
    public class KimedContext : DbContext
    {
        #region Ctor
        public KimedContext(DbContextOptions<KimedContext> options) : base(options)
        {

        }
        public KimedContext()
        {

        } 
        #endregion

        public virtual DbSet<Info> Info { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
