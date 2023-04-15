using EmprestimoDVD.Application;
using EmprestimoDVD.Infrastructure.Repository;
using EmprestimoDVD.Infrastructure.Service;
using EmprestimoDVD.Persistence;
using EmprestimoDVD.Persistence.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json.Serialization;

namespace EmprestimoDVD.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EmprestimoDVDContext>(
                context => context.UseSqlServer(Environment.GetEnvironmentVariable("database") ?? Configuration.GetConnectionString("database"),
                                                b =>
                                                {
                                                    b.MigrationsAssembly("EmprestimoDVD.Persistence");
                                                })
            );

            services.AddTransient<IAmigoRepository, AmigoRepository>();
            services.AddTransient<IAmigoService, AmigoService>();
            services.AddTransient<IDVDRepository, DVDRepository>();
            services.AddTransient<IEmprestimoRepository, EmprestimoRepository>();
            services.AddTransient<IGeneroRepository, GeneroRepository>();
            services.AddTransient<IGeneroService, GeneroService>();
            services.AddTransient<IPessoaRepository, PessoaRepository>();
            services.AddTransient<IPessoaService, PessoaService>();

            services.AddControllers()
                    .AddJsonOptions(options =>
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                    )
                    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "EmprestimoDVD.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmprestimoDVD.API v1"));
            }

            app.UseRouting();

            app.UseCors(x => x.AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<EmprestimoDVDContext>();
                context.Database.Migrate();
            }
        }
    }
}
