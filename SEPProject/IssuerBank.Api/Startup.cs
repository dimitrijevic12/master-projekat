using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Interface.Service;
using IssuerBank.Core.Services;
using IssuerBank.DataAccess.BankDbContext;
using IssuerBank.DataAccess.Implementation;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;

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
            services.AddControllers().AddNewtonsoftJson();
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
            services.AddTransient<CertificateValidation>();
            services.AddHttpClient();

            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate(options => {

                options.AllowedCertificateTypes = CertificateTypes.SelfSigned;
                options.Events = new CertificateAuthenticationEvents
                {
                    OnCertificateValidated = context => {
                        var validationService = context.HttpContext.RequestServices.GetService<CertificateValidation>();
                        if (validationService.ValidateCertificate(context.ClientCertificate))
                        {
                            context.Success();
                        }
                        else
                        {
                            context.Fail("Invalid certificate");
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context => {
                        context.Fail("Invalid certificate");
                        return Task.CompletedTask;
                    }
                };
            });

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

            app.UseAuthentication();

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