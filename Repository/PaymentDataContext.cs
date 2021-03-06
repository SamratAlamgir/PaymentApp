﻿using Microsoft.EntityFrameworkCore;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "server=.;database=PaymentDb;trusted_connection=true;";
                optionsBuilder.UseSqlServer(connectionString);
            }
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

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.CardNumberMasked)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.CurrencyCode)
                    .IsRequired();

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            });
        }
    }
}
