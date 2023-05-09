
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Service.Implementations;
using Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EmployeeManagementDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnectionString")));
// Add services to the container.
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IBenefitService,BenefitService>();
builder.Services.AddTransient<IPerformanceReviewService, PerformanceReviewService>();
builder.Services.AddTransient<IJobHistoryService, JobHistoryService>(); 
builder.Services.AddTransient<IDepartmentService,DepartmentService>();
builder.Services.AddTransient<IPayRollService, PayRollService>();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
