﻿using Data.Guest;
using Data.Room;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        public virtual DbSet<Domain.Guest.Entities.Guest> Guests { get; set; }
        public virtual DbSet<Domain.Room.Entities.Room> Rooms { get; set; }
        public virtual DbSet<Domain.Entities.Booking> Bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GuessConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
        }
    }
}
