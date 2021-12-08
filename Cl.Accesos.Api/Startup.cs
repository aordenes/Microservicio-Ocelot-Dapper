using Cl.Licencias.Medicas.Aplicacion.Contratos;
using Cl.Licencias.Medicas.Dal.ContextOracleDb;
using Cl.Licencias.Medicas.Dal.Helper;
using Cl.Licencias.Medicas.Dal.IRepositorioOracle;
using Cl.Licencias.Medicas.Dal.RepositorioOracle;
using Cl.Licencias.Medicas.Dominio.Servicios;
using Cl.Licencias.Medicas.Transversal.ExcepcionManager;
using Cl.Licencias.Medicas.Transversal.Log;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using System;
using System.IO;
using System.Text;

namespace Cl.Accesos.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Segmento para tomar el alrchivo de configuracion Nlog 
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/Nlog.config"));

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<ConexionConfiguracion>(
            option => {
                option.DefaultConnection = Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            }
            );
            
            services.AddTransient<IFactoryConnection, FactoryConnection>();
            services.AddScoped(typeof(IOracleRepository<>), typeof(OracleRepository<>));
            services.AddScoped(typeof(IAccesoContrato<>), typeof(AccesoContrato<>));
            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddControllers();
           

            services.AddAuthentication(options =>
            {
                //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.RequireHttpsMetadata = true;
                jwtBearerOptions.SaveToken = true;
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWTSettings:SecretKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cl.Minsal.Accesos.Api", Version = "v1" });
            });

            services.AddCors(opcion =>
            {
                opcion.AddPolicy("CorsRule", regla =>
                {
                    regla.AllowAnyHeader().AllowAnyMethod().WithOrigins("*").AllowAnyOrigin();//si es publico le doy *, si no le doy acceso a la url que yo quiero, asi la spersona spueden acceder a tu api
                });
            });

           
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cl.Minsal.Accesos.Api v1"));
            }

            //tener el mismo cors que se utiliza en la configuracion de la regla
            //estamso haciendo que los metodos sean expuestos de manera publica por el momento
            app.UseCors("CorsRule");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Manejo de errores globales - Nlog
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
