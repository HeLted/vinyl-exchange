using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using VinylExchange.Data.Models;


namespace VinylExchange.Data
{
    public class VinylExchangeDbContext : ApiAuthorizationDbContext<VinylExchangeUser>
    {
        public VinylExchangeDbContext(
           DbContextOptions options,
           IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<StyleRelease> StyleReleases { get; set; }        
        public DbSet<Release> Releases { get; set; }
        public DbSet<ReleaseFile> ReleaseFiles { get; set; }

        public DbSet<CollectionItem> Collections { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>()
                .HasMany(s => s.Styles)
                .WithOne(g => g.Genre)
                .HasForeignKey(g => g.GenreId);

            modelBuilder.Entity<StyleRelease>(styleRelease =>
            {
                styleRelease.HasKey(sr => new {sr.StyleId, sr.ReleaseId });

                styleRelease
                .HasOne(sr => sr.Style)
                .WithMany(s => s.Releases)
                .HasForeignKey(sr => sr.StyleId);

                styleRelease
                .HasOne(sr => sr.Release)
                .WithMany(r => r.Styles)
                .HasForeignKey(sr => sr.ReleaseId);

            });

            modelBuilder.Entity<ReleaseFile>()
                .HasOne(r => r.Release)
                .WithMany(rf => rf.ReleaseFiles)
                .HasForeignKey(r => r.ReleaseId);

            modelBuilder.Entity<CollectionItem>(collectionItem =>
            {

                collectionItem
                .HasOne(ci => ci.User)
                .WithMany(u => u.ReleaseCollection)
                .HasForeignKey(ci => ci.UserId);

                collectionItem
                .HasOne(ci=> ci.Release)
                .WithMany(r => r.ReleaseInCollections)
                .HasForeignKey(ci => ci.ReleaseId);
            });
               
            
        }

    }
}
