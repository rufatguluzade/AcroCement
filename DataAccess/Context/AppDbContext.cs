using Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) { }


        public DbSet<Contact> Contacts { get; set; }
        public DbSet<AboutUS> Abouts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<CustomerReaction> CustomerReactions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<WhyUs> WhyUs { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.Advantages)
                .HasConversion(
                    v => string.Join(',', v),    // Listeyi virgülle ayrılmış bir string'e dönüştür
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());  // String'i listeye dönüştür


            modelBuilder.Entity<Product>()
                .Property(e => e.AreasOfApplication)
                .HasConversion(
                    v => string.Join(',', v),    // Listeyi virgülle ayrılmış bir string'e dönüştür
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());  // String'i listeye dönüştür

        }
     



    }
}
