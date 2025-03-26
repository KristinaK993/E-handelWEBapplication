
using E_handelWEBapplication.Models;
using E_handelWEBapplication.Services;
using E_handelWEBapplication.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace E_handelWEBapplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();

            // Registrera FluentValidation validators
            builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            // Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<EHandelWebappContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<EHandelWebappContext>();

                var fakeDataService = new FakeDataService(dbContext);
                fakeDataService.SeedFakeData();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
