using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace QZ.WebAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //配置跨域请求
            services.AddCors(option=>option.AddPolicy("cors", policy=>policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().AllowAnyOrigin()));

            //集中注册服务
            //string directory = AppContext.BaseDirectory;
            // directory = directory.Replace("\\", "/");
            //int length = directory.IndexOf("/QZ.WebAPI");
            //directory = directory.Substring(0, length);
            //directory += "/QZ.Services/bin/Debug/netcoreapp2.1/QZ.Services.dll";
            string directory = "QZ.Services";
            foreach (var item in GetClassName(directory))
            {
                foreach (var typeArry in item.Value)
                {
                    services.AddScoped(typeArry, item.Key);
                }
            }

            //加载到容器中
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            IContainer container = containerBuilder.Build();

            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //将“CORS”（自己在 ConfigureServices 中的命名）中间件添加到Web应用程序管道以允许跨域要求。
            app.UseCors("cors");
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private Dictionary<Type, Type[]> GetClassName(string assemblyName)
        {
            if (!string.IsNullOrWhiteSpace(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                Type[] types = assembly.GetTypes();

                Dictionary<Type, Type[]> result = new Dictionary<Type, Type[]>();
                foreach (Type item in types.Where(x => !x.IsInterface))
                {
                    Type[] interfaceType = item.GetInterfaces();
                    result.Add(item, interfaceType);                    
                }
                return result;
            }
            return new Dictionary<Type, Type[]>();
        }
    }
}
