using AIMobile.Models.DataModels;
using AIMobileCus.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AIMobile.DAO
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
       public DbSet<BrandEntity> Brand { get; set; }
        public DbSet<TypeEntity> Type { get; set; }
        public DbSet<ProductEntity> Product { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<PaymentTypeEntity> Payment { get; set; }
        public DbSet<ShopEntity> Shop { get; set; }
        public DbSet<ImageEntity>Image { get; set; }
        public DbSet<ShopProductEntity> ShopProduct { get; set; }
        public DbSet<AdminRegisterEntity> Admin { get; set; }
        public DbSet<NotiEntity> Noti { get; set; }
        public DbSet<PurchaseEntity> Purchase { get; set; }
    }
}
