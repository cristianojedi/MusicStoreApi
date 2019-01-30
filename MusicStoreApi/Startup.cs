using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicStoreApi.Data;
using MusicStoreApi.Repository;
using MusicStoreApi.Repository.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace MusicStoreApi
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
            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddDbContext<MusicStoreDbContext>(options => options.UseInMemoryDatabase("MusicStoreDatabase"));

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1",
                        new Info
                        {
                            Title = "Artists Registry Api",
                            Version = "v1",
                            Contact = new Contact
                            {
                                Name = "Cristiano Rocha",
                                Email = "cristianojedi@gmail.com",
                                Url = "https://github.com/cristianojedi"
                            }

                        });

                    string pathName = System.AppContext.BaseDirectory;
                    string applicationName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                    string pathXmlDoc = Path.Combine(pathName, $"{applicationName}.xml");

                    c.IncludeXmlComments(pathXmlDoc);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable CORS requests
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Activated middlewares to use Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Artists Registry Api");
            });
        }
    }
}
