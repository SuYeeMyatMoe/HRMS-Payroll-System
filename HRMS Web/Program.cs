using HRMS_Web.DAO;
using HRMS_Web.Services;
using HRMS_Web.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("HRMSDB_Connection");
//Configure the DbContext to use SQL Server
builder.Services.AddDbContext<HRMSWebDBContext>(options => options.UseSqlServer(connectionString));
//and pass the connection string to the DbContext(options is a variable)

builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();//registering Unit Of Work dependency injection
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
