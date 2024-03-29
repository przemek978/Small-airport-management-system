﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SBD.Models;

namespace SBD.Data
{
    public class AirPortContext : DbContext
    {
        public AirPortContext(DbContextOptions<AirPortContext> options)
            : base(options)
        {
        }
        public DbSet<SBD.Models.Bagaz> Bagaz { get; set; }
        public DbSet<SBD.Models.Bilet> Bilet { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bagaz>().ToTable("BAGAZ");
            modelBuilder.Entity<Bilet>().ToTable("BILET");
            modelBuilder.Entity<LiniaLotnicza>().ToTable("LINIALOTNICZA");
            modelBuilder.Entity<Pilot>().ToTable("PILOT");
            modelBuilder.Entity<Pracownik>().ToTable("PRACOWNIK");
            modelBuilder.Entity<Samolot>().ToTable("SAMOLOT");
            modelBuilder.Entity<Licencja>().ToTable("LICENCJE");
            modelBuilder.Entity<Lotnisko>().ToTable("LOTNISKO");
            modelBuilder.Entity<Lot>().ToTable("LOT");
            modelBuilder.Entity<Pasazer>().ToTable("PASAZER");
            modelBuilder.Entity<Bagaz>()
            .HasOne(b => b.Bilet)
            .WithOne(i => i.Bagaz)
            .HasForeignKey<Bilet>(b => b.id_bagazu);
            modelBuilder.Entity<Lotnisko>(entity =>
            { 
                entity.HasMany(p => p.Przyloty)
                    .WithOne(d => d.Lotnisko_Koncowe)
                    .HasForeignKey(d => d.id_lotniska_koncowego);

                entity.HasMany(p => p.Odloty)
                    .WithOne(d => d.Lotnisko)
                    .HasForeignKey(d => d.id_lotniska_startowego);
            });

            modelBuilder.Entity<Pilot>(entity =>
            {
                entity.HasMany(p => p.Loty_Kapitana)
                    .WithOne(d => d.Kapitan)
                    .HasForeignKey(d => d.id_kapitana);

                entity.HasMany(p => p.Loty_Oficera)
                    .WithOne(d => d.Oficer)
                    .HasForeignKey(d => d.id_oficera);
            });

          

        }
        public DbSet<SBD.Models.Pasazer> Pasazer { get; set; }
        public DbSet<SBD.Models.Lot> Lot { get; set; }
        public DbSet<SBD.Models.Licencja> Licencja { get; set; }
        public DbSet<SBD.Models.LiniaLotnicza> LiniaLotnicza { get; set; }
        public DbSet<SBD.Models.Samolot> Samolot { get; set; }
        public DbSet<SBD.Models.Pilot> Pilot { get; set; }
        public DbSet<SBD.Models.Pracownik> Pracownik { get; set; }
        public DbSet<SBD.Models.Lotnisko> Lotnisko { get; set; }
    }
}
