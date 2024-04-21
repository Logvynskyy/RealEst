using Microsoft.EntityFrameworkCore;
using RealEst.DataAccess;
using RealEst.DataAccess.Implementations;
using RealEst.DataAccess.Interfaces;
using RealEst.Services.Services.Implementations;
using RealEst.Services.Services.Interfaces;

namespace RealEst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLogging(builder => builder.AddConsole());
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RealEstDB")));

            builder.Services.AddTransient<IUnitRepository, UnitRepository>();
            builder.Services.AddTransient<IUnitService, UnitService>();

            builder.Services.AddTransient<IContractRepository, ContractRepository>();
            builder.Services.AddTransient<IContractService, ContractService>();

            builder.Services.AddTransient<ITennantRepository, TennantRepository>();
            builder.Services.AddTransient<ITennantService, TennantService>();

            builder.Services.AddTransient<IContactRepository, ContactRepository>();
            builder.Services.AddTransient<IContactService, ContactService>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationContext>();
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    // Ensure database migration and apply pending migrations
                    context.Database.EnsureDeleted();
                    if(context.Database.EnsureCreated())
                        logger.LogInformation("Database migration completed.");

                    // Seed the database with initial data
                    EntitiesSeeder.EnsureSeeded(context);
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}