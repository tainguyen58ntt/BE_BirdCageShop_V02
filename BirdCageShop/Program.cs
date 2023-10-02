using BirdCageShopDbContext;
using BirdCageShopInterface;
using BirdCageShopInterface.IRepositories;
using BirdCageShopInterface.IServices;
using BirdCageShopReposiory;
using BirdCageShopReposiory.Repositories;
using BirdCageShopService;
using BirdCageShopService.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using BirdCageShopOther.Mapper;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





//LocalDB
builder.Services.AddDbContext<BirdCageShopContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);



// Repo

builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// Service 
builder.Services.AddScoped<IRoleService, RoleService>();


//
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//
builder.Services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);
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
