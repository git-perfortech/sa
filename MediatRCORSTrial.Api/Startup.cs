using FluentValidation;
using MediatR;
using MediatRCORSTrial.Api.Filters;
using MediatRCORSTrial.Core;
using MediatRCORSTrial.Core.Common.Contracts;
using MediatRCORSTrial.Core.Common.Extensions.Extensions;
using MediatRCORSTrial.Core.Common.Validation;
using MediatRCORSTrial.Core.PipelineBehaviors;
using MediatRCORSTrial.Data.Contracts;
using MediatRCORSTrial.Data.Data_Repositories;
using MediatRCORSTrial.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MediatRCORSTrial.Api
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
            

            //Expose HttpContext to services
            services.AddHttpContextAccessor();


            services.ConfigureContext(Configuration);
            services.ConfigureUnitOfWork();

            services.AddMediatR(typeof(GetAllProductsHandlers).Assembly);
            services.AddScoped<IProducstRepository, ProducstRepository>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly);

            //Register the filter in mvc pipeline
            //services.AddControllers(options => {
            //    options.Filters.Add<ValidationFilter>();
            //});

            


            services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });
            //services.AddMetrics();

            //services.AddMvc().AddFluentValidation();
            //services.ConfigureLogger(Configuration);
            //services.ConfigureContext(Configuration);
            //services.ConfigureAuthentication(Configuration);
            //services.ConfigureSystemParams(Configuration);
            //services.AddHttpContextAccessor();
            //services.IntegrationServices();
            //services.IntegrationMernisServices();
            //services.ConfigureMediatr();
            //services.ConfigureCommands();
            
            //services.ConfigureUnitOfWork();

            //services.ConfigureRepository();
            //services.ConfigureCors();
            //services.ConfigureSwagger();
            //services.AddAutoMapper(typeof(Startup));
            //services.ConfigureFluentValidation();
            //services.ConfigureRedisCache();
            //services.ConfigureJWT();
            //services.ConfigureFilter();
            //services.AddRabbitMQ(Configuration);

            services.AddControllers();
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
