using Albelli.Business.Helper;
using Albelli.Business.Services;
using Albelli.Business.Services.Interfaces;
using Albelli.Data;
using Albelli.Data.Entities;
using Albelli.Data.Interfaces.Services;
using Albelli.Data.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Albelli.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Albelli.Web", Version = "v1" });
            });

            services.AddDbContext<AlbelliDbContext>(
            options => options
            .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Albelli;Integrated Security=SSPI;")
            , ServiceLifetime.Transient);


            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterType<OrderApiService>().As<IOrderApiService>().InstancePerDependency();
            builder.RegisterType<OrderDataService>().As<IOrderDataService>().InstancePerDependency();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderItemService>().As<IOrderItemService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductTypeService>().As<IProductTypeService>().InstancePerLifetimeScope();


            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Albelli.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AlbelliDbContext>();
                context.Database.Migrate();

                //Insert Seed Data
                if (context.ProductType.Count() == 0)
                {
                    var model = new List<ProductType>();
                    model.Add(new ProductType() { Name = "photoBook", Width = 19 });
                    model.Add(new ProductType() { Name = "calendar", Width = 10 });
                    model.Add(new ProductType() { Name = "canvas", Width = 16 });
                    model.Add(new ProductType() { Name = "cards", Width = 4.7 });
                    model.Add(new ProductType() { Name = "mug", Width = 94 });
                    context.AddRange(model);
                    context.SaveChanges();
                }
            }
        }
    }

}
