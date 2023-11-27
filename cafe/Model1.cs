using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace cafe
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Bron> Bron { get; set; }
        public virtual DbSet<Stol> Stol { get; set; }
        public virtual DbSet<Waiter> Waiter { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.Education)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Bron>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Bron>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Bron>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Bron>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Stol>()
                .HasMany(e => e.Bron)
                .WithOptional(e => e.Stol)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Waiter>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Waiter>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Waiter>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Waiter>()
                .Property(e => e.Education)
                .IsUnicode(false);

            modelBuilder.Entity<Waiter>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Waiter>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
