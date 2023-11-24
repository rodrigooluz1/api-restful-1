using System.Text;
using API.DAL.IRepositories;
using API.DAL.Repositories;
using API.Domain.Entities;
using API.Domain.Mappers;
using API.Infra;
using API.Services;
using API.Util;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
//using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,
        },
        new List<string>()
        }
    });
});


builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddSingleton(typeof(INewsService), typeof(NewsService));
builder.Services.AddSingleton(typeof(IVideoService), typeof(VideoService));
builder.Services.AddSingleton(typeof(IGalleryService), typeof(GalleryService));
//builder.Services.AddSingleton(typeof(IMemoryCache), typeof(MemoryCache));    //trecho comentado pois alteramos o uso do cache para o REDIS
//builder.Services.AddSingleton(typeof(ICacheService), typeof(CacheMemoryService));
builder.Services.AddSingleton(typeof(ICacheService), typeof(CacheRedisService));

builder.Services.AddTransient<Upload>();


builder.Services.AddAutoMapper(typeof(EntityToViewModelMapping), typeof(ViewModelToEntityMapping));

builder.Services.AddCors();

builder.Services.AddTransient<TokenService>();
builder.Services.AddTransient<UserService>();
builder.Services.Configure<TokenManagement>(builder.Configuration.GetSection("tokenManagement"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey= new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                                    builder.Configuration.GetSection("tokenManagement:secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddDistributedRedisCache(option =>
{
    option.Configuration =
        builder.Configuration.GetSection("Redis:ConnectionString").Value;
});


#region HEALTHCHECK
builder.Services.AddHealthChecks()
            .AddRedis(builder.Configuration.GetSection("Redis:ConnectionString").Value, tags: new string[] { "db", "data" })
            .AddMongoDb(builder.Configuration.GetSection("DatabaseSettings:ConnectionString").Value
            + "/" + builder.Configuration.GetSection("DatabaseSettings:DatabaseName").Value,
            name: "mongodb", tags: new string[] { "db", "data" });

builder.Services.AddHealthChecksUI(opt =>
{
    opt.SetEvaluationTimeInSeconds(15);
    opt.MaximumHistoryEntriesPerEndpoint(60);
    opt.SetApiMaxActiveRequests(1);

    opt.AddHealthCheckEndpoint("default api", "/health");
}).AddInMemoryStorage();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region HEALTHCHECK
app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
}).UseHealthChecksUI(h => h.UIPath = "/healthui");
#endregion

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Medias")),
        RequestPath = "/medias"
});

app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.AllowAnyMethod();
    x.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();