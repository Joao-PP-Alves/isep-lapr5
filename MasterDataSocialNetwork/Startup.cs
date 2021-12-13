using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DDDNetCore.Infrastructure;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using DDDNetCore.Infrastructure.Users;
using DDDNetCore.Infrastructure.Introductions;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Connections;
using DDDNetCore.Infrastructure.Connections;
using DDDNetCore.Domain.Introductions;
using DDDNetCore.Domain.Tags;
using DDDNetCore.Infrastructure.Missions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using DDDNetCore.Infrastructure.Shared;
using DDDNetCore.Infrastructure.Tags;

// jdbc:sqlserver://vs398.dei.isep.ipp.pt\MYSQLSERVER:1433
namespace DDDNetCore
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
            services.AddCors(options =>
            {
                options.AddPolicy("MyAllowSpecificOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });

            services.AddDbContext<DddNetCoreDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .ReplaceService<IValueConverterSelector, StronglyEntityIdValueConverterSelector>());
            ConfigureMyServices(services);
            
            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("MyAllowSpecificOrigins");
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
            services.AddTransient<IUserService,UserService>();
            
            services.AddTransient<ITagRepository,TagRepository>();
            services.AddTransient<ITagService,TagService>();

            services.AddTransient<IMissionRepository,MissionRepository>();
            services.AddTransient<IMissionService,MissionService>();

            services.AddTransient<IConnectionRepository,ConnectionRepository>();
            services.AddTransient<IConnectionService,ConnectionService>();

            services.AddTransient<IIntroductionRepository,IntroductionRepository>();
            services.AddTransient<IIntroductionService,IntroductionService>();

            services.AddTransient<IFriendshipService,FriendshipService>();
        }
    }
}
