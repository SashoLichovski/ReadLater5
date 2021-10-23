﻿using Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ReadLaterDataContext : IdentityDbContext<User>

    {
        public ReadLaterDataContext(DbContextOptions<ReadLaterDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Bookmark> Bookmark { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
