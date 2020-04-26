using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PoTTr.Format.PoTTr.Data;

namespace PoTTr.Format.PoTTr
{
    public class PoTTrContext: DbContext
    {
        public DbSet<Agent>? Agents { get; set; }
        public DbSet<ContentNode>? ContentNode { get; set; }
        public DbSet<Name>? Names { get; set; }
        public DbSet<Metadata>? Metadata { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=.dbContext.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Metadata>().HasData(new Data.Metadata
            {
                Id = 1
            });
            modelBuilder.Entity<ContentNode>().HasData(new Data.ContentNode
            {
                Id = 1,
                MetadataId = 1,
                NodeType = NodeType.Root,
                ParentId = null
            }) ;
        }
    }
}
