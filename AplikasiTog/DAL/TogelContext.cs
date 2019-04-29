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
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }
        
        public virtual DbSet<User> Users { get; set; }
        
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Nomor> Nomors { get; set; }

        public virtual DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Transaction>()
                .HasRequired(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserID);

            modelBuilder.Entity<User>()
            .HasIndex(u => u.Name)
            .IsUnique();
        }
    }
}
