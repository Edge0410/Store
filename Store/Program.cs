using Store.Helpers;
using Microsoft.EntityFrameworkCore;
using Store.Helpers.Extensions;
using Store.Helpers.Middleware;
using Microsoft.OpenApi.Models;
using Store.Helpers.JwtUtils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// repositories

// Created each time they are requested
//builder.Services.AddTransient<IDatabaseRepository, DatabaseRepository>();
//// Created per client request
//builder.Services.AddScoped<IDatabaseRepository, DatabaseRepository>();
//// Created once per client request
//builder.Services.AddSingleton<IDatabaseRepository, DatabaseRepository>();

// services

//builder.Services.AddTransient<IDemoService, DemoService>();
// moved to extension
builder.Services.AddRepositories();
builder.Services.AddServices();
//builder.Services.AddSeeders();
builder.Services.AddUtils();

builder.Services.AddScoped<IJwtUtils, JwtUtils>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
//SeedData(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();
app.MapControllers();

app.Run();

/*void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<StudentsSeeder>();
        service.SeedIntialStudents();
    }
}*/