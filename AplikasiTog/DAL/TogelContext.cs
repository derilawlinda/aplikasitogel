using AplikasiTog.DAL.Models;
using GenericCodes.Core.DbContext;
using System.Data.Entity;

namespace AplikasiTog.DAL
{
    public partial class TogelContext : ApplicationDbContext
    {
        public TogelContext()
            : base("name=TogelContext")
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Transaction>()
                .HasRequired(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(s => s.UserID);
        }
    }
}
