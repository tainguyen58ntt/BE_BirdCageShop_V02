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
using BirdCageShopInterface.IValidator;
using BirdCageShopOther.Validator;
using BirdCageShopViewModel.Role;
using BirdCageShopDbContext.Models;
using Microsoft.AspNetCore.Identity;
using BirdCageShopViewModel.User;
using BirdCageShopViewModel.Voucher;
using BirdCageShopViewModel.Category;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//configure author jwt in swagger
builder.Services.AddSwaggerGen(option =>
{
	option.SwaggerDoc("v1", new OpenApiInfo { Title = "Bird Cage Shop", Version = "v1" });
	option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});
	option.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});
});





//LocalDB
builder.Services.AddDbContext<BirdCageShopContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//
builder.Services.AddSingleton<ITimeService, TimeService>();

// Repo

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

// Service 
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVourcherService, VourcherService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
//
builder.Services.AddScoped<IClaimService, ClaimService>();


//
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//
builder.Services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

// Validator
builder.Services.AddScoped<IRoleValidator, RoleValidator>();
builder.Services.AddScoped<RoleAddRule>();

builder.Services.AddScoped<IUserValidator, UserValidator>();
builder.Services.AddScoped<UserSignUpRule>();
builder.Services.AddScoped<UserChangePasswordRule>();

builder.Services.AddScoped<IVoucherValidator, VoucherValidator>();
builder.Services.AddScoped<VourcherAddRule>();

builder.Services.AddScoped<ICategoryValidator, CategoryValidator>();
builder.Services.AddScoped<CategoryCreateRule>();

builder.Services.AddHttpContextAccessor();
//

//
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	  .AddJwtBearer(options =>
	  {
		  options.RequireHttpsMetadata = false;
		  options.SaveToken = true;
		  options.TokenValidationParameters = new TokenValidationParameters()
		  {
			  ValidAudience = builder.Configuration["Jwt:Audience"],
			  ValidIssuer = builder.Configuration["Jwt:Issuer"],
			  ValidateIssuerSigningKey = true,
			  ValidateLifetime = true,
			  ClockSkew = TimeSpan.Zero,
			  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
			  //RoleClaimType = "role"
		  };
	  });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
