using Microsoft.EntityFrameworkCore;
using Booking_REST_API.Models;
using Booking_REST_API.DataAccess;
using DotNetEnv;

namespace Booking_REST_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Env.Load(".env");

            // Add services to the container.

            builder.Configuration.AddEnvironmentVariables();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<BookingContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddCors();

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
        }
    }
}
