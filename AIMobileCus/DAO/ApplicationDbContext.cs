﻿using AIMobile.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AIMobile.DAO
{
    public class ApplicationDbContext :DbContext
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
    }
}
