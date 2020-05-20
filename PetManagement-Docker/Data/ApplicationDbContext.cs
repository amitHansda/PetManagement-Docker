using Microsoft.EntityFrameworkCore;
using PetManager.Api.Entities.Configurations;
namespace PetManager.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<PetManager.Api.Entities.Pet> Pets{get;set;}
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder){           
            builder.ApplyConfiguration(new PetEntityTypeConfiguration());     
            base.OnModelCreating(builder);
        }
    }
}