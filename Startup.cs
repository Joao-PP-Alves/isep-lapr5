using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using DDDNetCore.Infrastructure;
using DDDNetCore.Infrastructure.Shared;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using DDDNetCore.Infrastructure.Users;
using DDDNetCore.Infrastructure.Missions;
using DDDNetCore.Infrastructure.Introductions;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Connections;
using DDDNetCore.Infrastructure.Connections;
using DDDNetCore.Domain.Introductions;
using Microsoft.Data.SqlClient;
// jdbc:sqlserver://vs398.dei.isep.ipp.pt\MYSQLSERVER:1433
namespace DDDNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
            // try 
            // { 
            //     SqlConnection connection = new SqlConnection();
            //     connection.ConnectionString =
            //         "Data Source=vs398.dei.isep.ipp.pt;" +
            //         "Initial Catalog=master;" +
            //         "User id=Zezoca;" +
            //         "Password=Tropita123;";
            //     
            //     
            //     //SqlConnection connection = new SqlConnection();
            //     //connection.ConnectionString = configuration.GetConnectionString("ConnectionString");
            //
            //         var sql = "CREATE TABLE teste3(id int primary key)";
            //         
            //     using var command = new SqlCommand(sql, connection);
            //     connection.Open();
            //     command.ExecuteNonQuery();
            // }
            // catch (SqlException e)
            // {
            //     Console.WriteLine(e.ToString());
            // }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DDDNetCoreDbContext>(opt =>
                opt.UseSqlServer("ConnectionString")
                .ReplaceService<IValueConverterSelector, StronglyEntityIdValueConverterSelector>());

            ConfigureMyServices(services);
            

            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureMyServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork,UnitOfWork>();

            services.AddTransient<IUserRepository,UserRepository>();
            services.AddTransient<UserService>();

            services.AddTransient<IFriendshipRepository,FriendshipRepository>();
            services.AddTransient<FriendshipService>();   
            
            services.AddTransient<IMissionRepository,MissionRepository>();
            services.AddTransient<MissionService>();

            services.AddTransient<IConnectionRepository,ConnectionRepository>();
            services.AddTransient<ConnectionService>();

            services.AddTransient<IIntroductionRepository,IntroductionRepository>();
            services.AddTransient<IntroductionService>();
        }
    }
}
