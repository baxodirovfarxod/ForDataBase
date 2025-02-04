using MusicCrudAsync.DataAccess; // MainContext uchun kerak
using MusicCrudAsync.Repository.Service;
using MusicCrudAsync.Service.Service;
using Microsoft.EntityFrameworkCore;

namespace MusicCrudAsync.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // **Add DbContext to DI container**
            builder.Services.AddDbContext<MainContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Agar connection string bo'lsa

            // **Add repository and service to DI**
            builder.Services.AddScoped<IMusicRepository, MusicRepositoryDB>();
            builder.Services.AddScoped<IMusicService, MusicService>();

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
