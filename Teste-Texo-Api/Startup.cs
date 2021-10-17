using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Teste_Texo_Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            }));

            services.AddControllers();

            services.AddDbContext<CatalogContext>(opt => opt.UseInMemoryDatabase("Catalog"));

            services.AddScoped(typeof(IRepositoryMovie), typeof(RepositoryMovie));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRepositoryMovie repositoryMovie)
        {
            if (!repositoryMovie.Any())
            {
                var dataBase = Configuration.GetSection("DataBase").Value;
                var dir = AppDomain.CurrentDomain.BaseDirectory;
                var path = @$"{dir}{dataBase}";
                Console.WriteLine($"Load DataBase: {path}");
                repositoryMovie.LoadData(path);
            }

            app.UseCors("AllowAll");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
