using AIMobile.DAO;
using AIMobile.Services.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TOMS.Areas.Identity.Pages.Account;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddRazorPages();
var config = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(config.GetConnectionString("AIMobileDB")));
builder.Services.AddDefaultIdentity<IdentityUser>(o => o.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>(o =>
//{
//    o.SignIn.RequireConfirmedAccount = false;
//    o.Password.RequireDigit = true;
//    o.Password.RequiredLength = 8;
//    o.Password.RequireUppercase = true;
//    o.Password.RequireLowercase = true;
//}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI().AddDefaultTokenProviders();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ITypeServices, TypeServices>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IShopProductService, ShopProductService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});
app.Run();
