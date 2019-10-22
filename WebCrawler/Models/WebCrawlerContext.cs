using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Models
{
    public partial class WebCrawlerContext : DbContext
    {
        public WebCrawlerContext()
        {
        }

        public WebCrawlerContext(DbContextOptions<WebCrawlerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LibyanTenders> LibyanTenders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<LibyanTenders>(entity =>
            {
                entity.HasKey(e => e.TenderId);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Field)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Title)
                    .IsRequired();
            });
        }
    }
}
