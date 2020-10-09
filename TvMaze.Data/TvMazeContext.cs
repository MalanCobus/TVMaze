using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TvMaze.Data
{
    public class TvMazeContext : DbContext
    {
        public TvMazeContext(DbContextOptions<TvMazeContext> options) : base(options)
        {
        }

        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<Castmember> Castmember { get; set; }
        public DbSet<CastShowLinkage> CastShowLinkage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CastShowLinkage>()
                .HasKey(s => new { s.TVShowId, s.CastmemberId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
