﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste_Texo_Api
{
    public class CatalogContext : DbContext
    {

        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {                    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)      
        {
            #region ConfigureItem
            var splitStringConverter = new ValueConverter<List<string>, string>(v => string.Join(",", v), v => v.Split(new[] { ',' }).ToList());
            modelBuilder.Entity<MovieItem>(
                b =>
                {
                    //b.Property("_id");
                    //b.HasKey("_id");
                    //b.Property(e => e.Producers).;
                    //b.Ignore(e => e.Producers);
                    b.Property(nameof(MovieItem.Studios)).HasConversion(splitStringConverter);
                    b.Property(nameof(MovieItem.Producers)).HasConversion(splitStringConverter);
                    //b.HasMany(e => e.Tags).WithOne().IsRequired();
                });
            #endregion
        }

        public DbSet<MovieItem> TitleItems { get; set; }
    }
}
