using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.Linq;

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
            var listValueComparer = new ValueComparer<List<string>>(
                (d1, d2) => d1.SequenceEqual(d2),
                d => d.GetHashCode(),
                d => d.ToList<string>());
            modelBuilder.Entity<MovieItem>(
                b =>
                {
                    b.Property(nameof(MovieItem.Studios))
                        .HasConversion(splitStringConverter)
                        .Metadata
                        .SetValueComparer(listValueComparer);
                    b.Property(nameof(MovieItem.Producers))
                        .HasConversion(splitStringConverter)
                        .Metadata
                        .SetValueComparer(listValueComparer);
                });
            #endregion
        }

        public DbSet<MovieItem> TitleItems { get; set; }
    }
}
