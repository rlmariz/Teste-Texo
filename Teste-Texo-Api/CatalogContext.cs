using Microsoft.EntityFrameworkCore;
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
            modelBuilder.Entity<MovieItem>(
                b =>
                {
                    b.Property(nameof(MovieItem.Studios)).HasConversion(splitStringConverter);
                    b.Property(nameof(MovieItem.Producers)).HasConversion(splitStringConverter);
                });
            #endregion
        }

        public DbSet<MovieItem> TitleItems { get; set; }
    }
}
