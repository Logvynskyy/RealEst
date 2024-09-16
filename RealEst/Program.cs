using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEst.DataAccess;
using RealEst.DataAccess.Implementations;
using RealEst.DataAccess.Interfaces;
using RealEst.Services.Services.Implementations;
using RealEst.Services.Services.Interfaces;
using RealEst.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace RealEst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            // Add services to the container.
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = config["JwtSettings:Issuer"],
                    ValidAudience = config["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLogging(builder => builder.AddConsole());
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(config.GetConnectionString("RealEstDB")));

            builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

            builder.Services.AddTransient<IOrganisationRepository, OrganisationRepository>();
            builder.Services.AddTransient<IOrganisationService, OrganisationService>();

            builder.Services.AddTransient<IUnitRepository, UnitRepository>();
            builder.Services.AddTransient<IUnitService, UnitService>();

            builder.Services.AddTransient<IContractRepository, ContractRepository>();
            builder.Services.AddTransient<IContractService, ContractService>();

            builder.Services.AddTransient<ITennantRepository, TennantRepository>();
            builder.Services.AddTransient<ITennantService, TennantService>();

            builder.Services.AddTransient<IContactRepository, ContactRepository>();
            builder.Services.AddTransient<IContactService, ContactService>();

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationContext>();
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    // Ensure database migration and apply pending migrations
                    //context.Database.EnsureDeleted();
                    if(context.Database.EnsureCreated())
                        logger.LogInformation("Database migration completed.");

                    // Seed the database with initial data
                    //EntitiesSeeder.EnsureSeeded(context);
                    logger.LogInformation("Database seeding completed.");
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while setting up the database.");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });


            app.MapControllers();

            app.Run();
        }
    }
}