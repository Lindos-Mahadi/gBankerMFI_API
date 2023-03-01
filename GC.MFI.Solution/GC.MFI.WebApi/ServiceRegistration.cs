using GC.MFI.DataAccess;
using GC.MFI.DataAccess.InfrastructureBase;
using GC.MFI.DataAccess.Repository.Implementations;
using GC.MFI.DataAccess.Repository.Interfaces;
using GC.MFI.DataAccess.Repository.Pos.Implementations;
using GC.MFI.DataAccess.Repository.Pos.Interfaces;
using GC.MFI.Models;
using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using GC.MFI.Models.Modules.Distributions.Security;
using GC.MFI.Security.Jwt;
using GC.MFI.Security.Models;
using GC.MFI.Services.Modules.BntPos.Implementations;
using GC.MFI.Services.Modules.BntPos.Interfaces;
using GC.MFI.Services.Modules.Email.Implementations;
using GC.MFI.Services.Modules.Email.Interfaces;
using GC.MFI.Services.Modules.GcMfi.Implementations;
using GC.MFI.Services.Modules.GcMfi.Interfaces;
using GC.MFI.Services.Modules.Security.Implementations;
using GC.MFI.Services.Modules.Security.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Polly.Extensions.Http;
using System.Text;
using Twilio.Clients;

namespace GC.MFI.WebApi
{
    public class ServiceRegistration
    {
        public static void RegisterService(IServiceCollection services, IConfiguration Configuration)
        {
           
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
             Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("GC.MFI.DataAccess")));

            services.AddDbContext<GBankerDbContext>(options => options.UseSqlServer(
             Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("GC.MFI.DataAccess")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                //Other options go here
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
            services.AddControllersWithViews();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));
          
            var settings = new JWT();
            Configuration.Bind("JWT", settings);
            var key = Encoding.UTF8.GetBytes(settings.SecretKey);

            // TWILIO SMS INTEGRATION
            services.AddScoped<ISMSTwilioService, SMSTwilioService>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddTransient<IEmailHelper, EmailHelper>();
            // configure DI for application services
    
            services.AddMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".bizzntekerp.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IDatabaseFactory, DatabaseFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtTokenHelper, JwtTokenHelper>();            

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            // Portal Member Dependancy
            services.AddScoped<IPortalMemberRepository, PortalMemberRepository>();
            services.AddScoped<IPortalMemberService, PortalMemberService>();

            services.AddScoped<IOfficeRepository, OfficeRepository>();
            services.AddScoped<IOfficeService, OfficeService>();

            // Member Dependancy
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMemberService, MemberService>();

            // Country Dependancy
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICountryService, CountryService>();

            // Upozilla Dependancy
            services.AddScoped<IUpozillaRepository, UpozillaRepository>();
            services.AddScoped<IUpozillaService, UpozillaService>();

            // District Dependancy
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IDistrictService, DistrictService>();


            // NID Dependancy
            services.AddScoped<INIDService, NIDService>();

            services.AddScoped<IStoredProcedureRepository, StoredProcedureRepository>();
            services.AddScoped<IStoredProcedureService, StoredProcedureService>();

            // Union Dependancy
            services.AddScoped<IUnionRepository, UnionRepository>();
            services.AddScoped<IUnionService, UnionService>();

            // NIDLogging Dependancy
            services.AddScoped<INIDLoggingRepository, NIDLoggingRepository>();
            services.AddScoped<INIDLoggingService, NIDLoggingService>();

            // Portal Loan Summary Dependancy
            services.AddScoped<IPortalLoanSummaryRepository, PortalLoanSummaryRepository>();
            services.AddScoped<IPortalLoanSummaryService, PortalLoanSummaryService>();

            // Portal Loan Summary Dependancy
            services.AddScoped<IPurposeRepository, PurposeRepository>();
            services.AddScoped<IPurposeService, PurposeService>();

            // NIDLogging Dependancy
            services.AddScoped<IMemberPassBookRegisterRepository, MemberPassBookRegisterRepository>();
            services.AddScoped<IMemberPassBookRegisterService, MemberPassBookRegisterService>();

            // Investor Dependancy
            services.AddScoped<IInvestorRepository, InvestorRepository>();
            services.AddScoped<IInvestorService, InvestorService>();

            // PortalSavingSummary Dependancy
            services.AddScoped<IPortalSavingSummaryRepository, PortalSavingSummaryRepository>();
            services.AddScoped<IPortalSavingSummaryService, PortalSavingSummaryService>();

            // NomineeXPortalSavingSummary Dependancy
            services.AddScoped<INomineeXPortalSavingSummaryRepository, NomineeXPortalSavingSummaryRepository>();
            services.AddScoped<INomineeXPortalSavingSummaryService, NomineeXPortalSavingSummaryService>();

            // File Upload Dependancy
            services.AddScoped<IFileUploadRepository, FileUploadRepository>();
            services.AddScoped<IFileUploadService, FileUploadService>();

            //Savings Acc close dependancy
            services.AddScoped<ISavingsAccCloseRepository, SavingsAccCloseRepository>();
            services.AddScoped<ISavingsAccCloseService, SavingsAccCloseService>();

            //Loan Acc Close dependancy
            services.AddScoped<ILoanAccRescheduleRepository, LoanAccRescheduleRepository>();
            services.AddScoped<ILoanAccRescheduleService, LoanAccRescheduleService>();

            //SMS Log Table dependancy
            services.AddScoped<ISMSLogTableRepository, SMSLogTableRepository>();
            services.AddScoped<ISMSLogTableService, SMSLogTableService>();


            // Dashbord dependancy
            services.AddScoped<IDashboardService, DashboardService>();

            // Email Notification dependancy
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddScoped<IMailService, MailService>();


            // Register for model Validation
            services.AddControllers(
                options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

        }
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(response => response.IsSuccessStatusCode == false)
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
    
}
