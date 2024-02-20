using AIMobile.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace AIMobile.DAO
{
    public class ApplicationDbContext:DbContext
    {
public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) { 
        
        
        }
        public DbSet<BrandEntity> Brand { get; set; }  
        public DbSet<TypeEntity> Type { get; set; }
    }
}
