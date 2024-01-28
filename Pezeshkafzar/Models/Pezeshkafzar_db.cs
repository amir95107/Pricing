using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Pezeshkafzar.Models
{
    public partial class Pezeshkafzar_db : DbContext
    {
        public Pezeshkafzar_db()
            : base("name=Pezeshkafzar")
        {
        }


        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Product>()
                .Property(e => e.ShortDescription)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .Property(e => e.ImageName)
                .IsUnicode(false);

            
        }
    }
}
