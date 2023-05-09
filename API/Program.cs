using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Service.Implementations;
using Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EmployeeManagementDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnectionString")));
// Add services to the container.
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IBenefitService, BenefitService>();
builder.Services.AddTransient<IPerformanceReviewService, PerformanceReviewService>();
builder.Services.AddTransient<IJobHistoryService, JobHistoryService>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IPayRollService, PayRollService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
