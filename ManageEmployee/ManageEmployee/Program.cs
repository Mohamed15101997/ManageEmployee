using ManageEmployee.Core.Interfaces;
using ManageEmployee.Core.Models;
using ManageEmployee.EF.Data.Contexts;
using ManageEmployee.EF.Data.DataSeed;
using ManageEmployee.EF.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ManageEmployee
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add Connection String.
			var connectionString = builder.Configuration.GetConnectionString("defaultConnection")
				?? throw new InvalidOperationException("Connection Failed");
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(connectionString,
					b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
			});

			// Add services to the container.
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 6;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
			})
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();

			//builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IAccountRepository, AccountRepository>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();

			// Configure JWT Authentication
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = builder.Configuration["JWT:IssuerIP"],

					ValidateAudience = true,
					ValidAudience = builder.Configuration["JWT:AudienceIP"],

					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
						builder.Configuration["JWT:SecritKey"]))
				};
			});

			// Configure CORS
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAllOrigins",
					builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			});

			builder.Services.AddControllers();
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

			app.UseCors("AllowAllOrigins");
			app.UseAuthorization();

			app.MapControllers();

			// Add seed data
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				await SeedData.InitializeAsync(services);
			}

			app.Run();
		}
	}
}
