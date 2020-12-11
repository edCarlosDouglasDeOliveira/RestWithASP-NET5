using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestWithASP_NET5.Model.Context;
using RestWithASP_NET5.Business;
using RestWithASP_NET5.Business.Implementations;
using RestWithASP_NET5.Repository;
using RestWithASP_NET5.Repository.Implementations;

namespace RestWithASP_NET5
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
            
            // Conexão com o banco de dados
            var connection = Configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySqlContext>(options => options.UseMySql(connection));

            //Versionamento de api
            services.AddApiVersioning();

            //Injeção de dependência
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
