using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class PaymentDataContext : DbContext
    {
        public PaymentDataContext()
        {

        }

        public PaymentDataContext(DbContextOptions<PaymentDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Merchant> Merchants { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }

        public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=PaymentDb;Integrated Security=SSPI;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Merchant>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PaymentDetail>(entity =>
            {
                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            });
        }
    }
}
