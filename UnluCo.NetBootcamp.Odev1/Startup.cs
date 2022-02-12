using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.DBOperations;
using UnluCo.NetBootcamp.Odev5.Entities.Concrete;
using UnluCo.NetBootcamp.Odev5.Filters;
using UnluCo.NetBootcamp.Odev5.Middlewares;
using UnluCo.NetBootcamp.Odev5.Services;

namespace UnluCo.NetBootcamp.Odev5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //[Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers(config =>
            {
                config.Filters.Add(new HeaderFilter());
            });
            services.AddSwaggerGen(c =>
            {   
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UnluCo.NetBootcamp.Odev1", Version = "v1" });
                //c.DescribeAllEnumsAsStrings();
            });
            services.AddMemoryCache();
            services.AddResponseCaching();
            services.AddDistributedRedisCache(opt => 
            {
                opt.Configuration = "";//Host adresi verilir
                opt.InstanceName = "";//Hangi isimle cache yapilacaksa
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<ILoggerService, DBLoggerService>();//Program icerisinde ILoggerService istenirse DBLoggerService aktif edilir.
            services.AddDbContext<CarSystemDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<CarSystemDbContext>().AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),//key degerini byte cevirdik
                    ClockSkew = TimeSpan.Zero//zaman farklari olusabilecegi icin kullanilir
                };
            });
            services.AddScoped<TokenGenerator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UnluCo.NetBootcamp.Odev1 v1"));
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseResponseCaching();

            app.UseGlobalExceptionMiddleware();//program akisi icerisinde kendi yazdigim middleware devreye girer

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
