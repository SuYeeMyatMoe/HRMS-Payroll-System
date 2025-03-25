using HRMS_Web.DAO;
using HRMS_Web.Services;
using HRMS_Web.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("HRMSDB_Connection");
//Configure the DbContext to use SQL Server
builder.Services.AddDbContext<HRMSWebDBContext>(options => options.UseSqlServer(connectionString));

//for identity
builder.Services.AddRazorPages();//Register for Identity UIs
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<HRMSWebDBContext>();//Register for identity dbcontext for identity users and roles



// for repository pattern
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();//registering Unit Of Work dependency injection
//cannot use with AddSingleton must use with AddScoped since it is request based service!!

builder.Services.AddTransient<IPositionService, PositionService>();//registering PositionService dependency injection
//builder.Services.AddTransient<IDepartmentService, DepartmentService>();


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

app.UseRouting();

//enable authentication and authorization process
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();//mapping razor page routes
app.Run();
