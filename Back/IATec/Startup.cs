using IATec.Application.Services;
using IATec.Domain.Interfaces.Repositories;
using IATec.Domain.Interfaces.Services;
using IATec.Infra.Context;
using IATec.Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IATec.Api
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
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(i => i.FullName);
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "IATec",
                    Version = "v1",
                    Description = "Desafio",
                });
            });

            #region [Services]
            services.AddScoped<IVendaService, VendaService>();
            #endregion

            #region [Repository]
            services.AddScoped<IVendaRepository, VendaRepository>();
            services.AddDbContext<DatabaseContext>(_ =>
            {
                _.UseInMemoryDatabase("IATecDB");
                _.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(option => {
                option.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "IATec");
                options.RoutePrefix = "api-docs";
            });
        }
    }
}
