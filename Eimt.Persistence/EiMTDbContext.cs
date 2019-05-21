using Eimt.Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eimt.Persistence
{
    public class EiMTDbContext:DbContext
    {
        public EiMTDbContext(DbContextOptions options):base(options)
        {
                
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserConfirmationToken> Tokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRoles>()
                .HasKey(x => new { x.RoleId, x.UserId });
            modelBuilder.Entity<UserRoles>()
                .HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<UserRoles>()
                .HasOne(x => x.User)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();
            modelBuilder.Entity<Role>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<UserConfirmationToken>()
                .Property(x => x.CreateAt)
                .ValueGeneratedOnAdd();

        }
    }
}
