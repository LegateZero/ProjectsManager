
using Microsoft.EntityFrameworkCore;
using ProjectsManager.DAL;
using ProjectsManager.DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

//using LibraryDB = ProjectsManager.DAL.Context.ProjectsManagerDb;

namespace ProjectsManager.WebApi
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
               var connectionString = builder.Configuration.GetConnectionString("ProjectsManagerDbConnection") ?? throw new InvalidOperationException("Connection string 'ProjectsManagerDbConnection' not found.");

			// Add services to the container.
			//builder.Host.ConfigureAppConfiguration();
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddRepositories();
			builder.Services.AddAuthorization();
			builder.Services.AddDbContext<ProjectsManagerDb>(options =>
			{
				options.UseSqlServer(
					builder.Configuration
						.GetSection("Database")
						.GetConnectionString("MSSQL"));
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
				using (var scope = app.Services.CreateScope())
				{
					var db = scope.ServiceProvider.GetService<ProjectsManagerDb>();
					db.Database.Migrate();

				}
			}

			app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
			
		}

		
	}
}