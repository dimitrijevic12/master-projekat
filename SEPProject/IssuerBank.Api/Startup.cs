using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Interface.Service;
using IssuerBank.Core.Services;
using IssuerBank.DataAccess.BankDbContext;
using IssuerBank.DataAccess.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace IssuerBank.Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IssuerBank.Api", Version = "v1" });
            });

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IMerchantRepository, MerchantRepository>();
            services.AddScoped<IPaymentCardRepository, PaymentCardRepository>();
            services.AddScoped<IPSPRequestRepository, PSPRequestRepository>();
            services.AddScoped<IPSPResponseRepository, PSPResponseRepository>();
            services.AddScoped<IRegisteredUserRepository, RegisteredUserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IPaymentCardRepository, PaymentCardRepository>();
            services.AddScoped<IMerchantService, MerchantService>();
            services.AddScoped<IPSPRequestService, PSPRequestService>();
            services.AddScoped<IPSPResponseService, PSPResponseService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IPaymentCardService, PaymentCardService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddHttpClient();

            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("bankdb"))
                .UseLazyLoadingProxies());

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IssuerBank.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}