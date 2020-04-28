using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PoTTr.Format.PoTTr.Data;

namespace PoTTr.Format.PoTTr
{
    public class PoTTrContext : DbContext
    {
        public DbSet<Agent>? Agents { get; set; }
        public DbSet<ContentNode>? ContentNode { get; set; }
        public DbSet<Name>? Names { get; set; }
        public DbSet<Metadata>? Metadata { get; set; }
        public DbSet<ProjectData>? ProjectData { get; set; }

        public PoTTrContext() : base() { }
        public PoTTrContext(DbContextOptions<PoTTrContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite("Data Source=.dbContext.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Metadata>().HasData(new Metadata
            {
                Id = 1
            });
            modelBuilder.Entity<ContentNode>().HasData(new ContentNode
            {
                Id = 1,
                MetadataId = 1,
                NodeType = NodeType.Root,
                ParentId = null
            });

            modelBuilder.Entity<ProjectData>().HasData(
                new ProjectData { DataKey = "ProjectName", DataValue = string.Empty },
                new ProjectData { DataKey = "ProjectDate", DataValue = string.Empty },
                new ProjectData { DataKey = "ProjectAuthor", DataValue = string.Empty });
        }
    }
}
