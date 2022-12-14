using DAL.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public partial class RailWayDB : DbContext
    {
        public RailWayDB()
            : base("name=RailWayDB")
        {
        }

        public virtual DbSet<Passenger> Passenger { get; set; }
        public virtual DbSet<Seat> Seat { get; set; }
        public virtual DbSet<Station> Station { get; set; }
        public virtual DbSet<StationTrainSchedule> StationTrainSchedule { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<TimesForStation> TimesForStation { get; set; }
        public virtual DbSet<Train> Train { get; set; }
        public virtual DbSet<TypeOfVan> TypeOfVan { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Track> Track { get; set; }
        public virtual DbSet<Van> Van { get; set; }
        public virtual DbSet<CellStructureVan> CellStructureVan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeOfVan>().HasEntitySetName("Id");
            modelBuilder.Entity<CellStructureVan>().HasKey(i => new { i.NumberOfCell, i.NumberOfRow, i.TypeOfVanId });
            modelBuilder.Entity<TypeOfVan>().HasMany(e => e.CellStructureVan)
                .WithRequired(e => e.TypeOfVan).HasForeignKey(i => i.TypeOfVanId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Passenger>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.Passenger)
                .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Passenger>().HasOptional(e => e.User).WithMany(e => e.Passenger);

            modelBuilder.Entity<Seat>().HasKey(e => new { e.Id });
            modelBuilder.Entity<Seat>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.Seat).HasForeignKey(i=> i.SeatId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Station>()
                .HasMany(e => e.StationTrainSchedule)
                .WithRequired(e => e.Station)
                .HasForeignKey(e => e.IdStation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimesForStation>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.TimesForStation)
                .HasForeignKey(e => e.IdTimesForStationSource)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimesForStation>()
                .HasMany(e => e.Ticket1)
                .WithRequired(e => e.TimesForStation1)
                .HasForeignKey(e => e.IdTimesForStationDestiny)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Train>()
                .HasMany(e => e.StationTrainSchedule)
                .WithRequired(e => e.Train)
                .HasForeignKey(e => e.IdTrain)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Train>()
                .HasMany(e => e.Van)
                .WithRequired(e => e.Train)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Train>()
                .HasMany(e => e.Track)
                .WithRequired(e => e.Train)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Track>()
                .HasMany(e => e.TimesForStation)
                .WithRequired(e => e.Track)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<TypeOfVan>()
                .HasMany(e => e.Van)
                .WithRequired(e => e.TypeOfVan)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<TypeOfVan>()
                .HasMany(e => e.Seat)
                .WithRequired(e => e.TypeOfVan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Ticket)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<User>()
                .HasMany(e => e.Passenger)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ticket>().HasOptional(e=>e.User);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Train)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.IdUserCreator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserType>()
                .HasMany(e => e.User)
                .WithRequired(e => e.UserType)
                .HasForeignKey(e => e.TypeOfUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
