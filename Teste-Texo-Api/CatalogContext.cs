using Microsoft.EntityFrameworkCore;
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
            //#region ConfigureItem
            //modelBuilder.Entity<TitleItem>(
            //    b =>
            //    {
            //        b.Property("_id");
            //        b.HasKey("_id");
            //        b.Property(e => e.Name);
            //        b.HasMany(e => e.Tags).WithOne().IsRequired();
            //    });
            //#endregion
        }

        public DbSet<TitleItem> TitleItems { get; set; }
    }
}
