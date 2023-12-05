namespace cafe
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class cafeEntities : DbContext
    {
        public cafeEntities()
            : base("name=cafeEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Bron> Bron { get; set; }
        public virtual DbSet<ConfirmedBooking> ConfirmedBooking { get; set; }
        public virtual DbSet<Stol> Stol { get; set; }
        public virtual DbSet<Waiter> Waiter { get; set; }
    }
}