using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LoginRegister.Data;
using LoginRegister.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("LoginDbContextConnection") ?? throw new InvalidOperationException("Connection string 'LoginDbContextConnection' not found.");

builder.Services.AddDbContext<LoginDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>
    (options => {
        //options.SignIn.RequireConfirmedAccount = true;
        options.SignIn.RequireConfirmedAccount = false;
      
    }
    



    ).
    AddEntityFrameworkStores<LoginDbContext>().AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequireUppercase = false;
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
