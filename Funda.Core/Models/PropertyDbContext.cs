using Microsoft.EntityFrameworkCore;

namespace Funda.Core.Models
{
    /// <summary>
    /// Application database model
    /// </summary>
    public class PropertyDbContext : DbContext
    {
        public virtual DbSet<TagType> TagTypes { get; set; }
        public virtual DbSet<EstateProperty> EstateProperties { get; set; }
        public virtual DbSet<PropertyTag> PropertyTags { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }

        public PropertyDbContext()
        {
        }

        public PropertyDbContext(DbContextOptions<PropertyDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Since application uses inmemory database, this method injects initial data required to query Funda Api 
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Tags are hierarchycal. First group is used for city queries
            modelBuilder.Entity<TagType>().HasData(new TagType { Id = -1, ParentTagId = null, Name = "Everywhere", Code = "heel-nederland" });
            modelBuilder.Entity<TagType>().HasData(new TagType { Id = 1001, ParentTagId = -1, Name = "Amsterdam", Code = "amsterdam" });
            modelBuilder.Entity<TagType>().HasData(new TagType { Id = 1002, ParentTagId = -1, Name = "Utrecht", Code = "utrecht" });
            modelBuilder.Entity<TagType>().HasData(new TagType { Id = 1003, ParentTagId = -1, Name = "Hilversum", Code = "hilversum" });
            //Second group is used for options
            modelBuilder.Entity<TagType>().HasData(new TagType { Id = 1, ParentTagId = null, Name = "All", Code = "" });
            modelBuilder.Entity<TagType>().HasData(new TagType { Id = 10, ParentTagId = 1, Name = "Balcony", Code = "balkon" });
            modelBuilder.Entity<TagType>().HasData(new TagType { Id = 20, ParentTagId = 1, Name = "Roof Terrace", Code = "dakterras" });
            modelBuilder.Entity<TagType>().HasData(new TagType { Id = 30, ParentTagId = 1, Name = "Garden", Code = "tuin" });
            modelBuilder.Entity<TagType>().HasData(new TagType { Id = 31, ParentTagId = 1, Name = "Plot 250+", Code = "250+woonopp" });
            modelBuilder.Entity<TagType>().HasData(new TagType { Id = 32, ParentTagId = 1, Name = "Plot 500+", Code = "500+woonopp" });
        }
    }
}