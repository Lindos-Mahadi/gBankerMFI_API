using GC.MFI.DataAccess;
using GC.MFI.Models;
using GC.MFI.WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using GC.MFI.Security.Models;
using AutoMapper;
using GC.MFI.Models.Mapper;
using GC.MFI.WebApi.Filters;
using GC.MFI.WebApi.Middleware;
using Microsoft.AspNetCore;
using GC.MFI.Services.Modules.Firebase.Interfaces;
using GC.MFI.Services.Modules.Firebase.Implementations;
using System.Configuration;
using GC.MFI.Models.Models;

var builder = WebApplication.CreateBuilder(args);
string MyAllowSpecificOrigins = "CorsPolicy";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MisSetting>(
    builder.Configuration.GetSection("MisSetting"));

builder.Services.AddSingleton<IMisSetting>(serviceProvider =>
        serviceProvider.GetRequiredService<IOptions<MisSetting>>().Value);
var misSetting = new MisSetting();
builder.Configuration.Bind("MisSetting", misSetting);

builder.Services.Configure<JWT>(
    builder.Configuration.GetSection("JWT"));
builder.Services.AddSingleton<IJWT>(serviceProvider =>
        serviceProvider.GetRequiredService<IOptions<JWT>>().Value);

 static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<ServiceRegistration>()
        .ConfigureLogging((ctx, logging) =>
        {
            logging.AddConfiguration(ctx.Configuration.GetSection("Logging"));
           
        });

// Added for model validation
builder.Services.AddScoped<ValidationFilterAttribute>();

// Firebase Notifcation service
// Configure strongly typed settings objects
var appSettingsSection = builder.Configuration.GetSection("FcmNotification");
builder.Services.Configure<FcmNotificationSetting>(appSettingsSection);

// Add FirebaseApp Singleton
var webroot = builder.Environment.ContentRootPath;
builder.Services.AddFirebaseAppSingleton($"{webroot}/Firebase_key/key.json");



builder.Services.AddSingleton<IFirebaseNotificationService, FirebaseNotificationService>();

builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
});

builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(mc => {
    mc.AddProfile(new MapperProfileViewModelToDb());
    mc.AddProfile(new MapperProfileDbToViewModel());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper); 

if (!string.IsNullOrEmpty(misSetting?.CORSEnabledUrl))
{
    var urls = misSetting.CORSEnabledUrl.Split(',');
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(MyAllowSpecificOrigins,
            builder =>
            {
                builder.WithOrigins(urls)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            });
    });


}
ServiceRegistration.RegisterService(builder.Services, builder.Configuration);
var app = builder.Build();
app.UseMiddleware<LoggingMiddleware>();

// Configure the HTTP request pipeline.
app.UseRouting();




app.UseSwagger();
app.UseSwaggerUI();
app.UseSession();
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();

app.MapControllers();
app.MapHub<ChatHub>("/chathub");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
