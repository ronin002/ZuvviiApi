using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using ZuvviiAPI.Data;
using ZuvviiAPI.Repository;
using ZuvviiAPI.Repository.Impl;
using ZuvviiAPI.StorageServices;
using ZuvviiAPI.Services;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureMVC(builder);
ConfigureAuthentication(builder);

ConfigureData(builder);
ConfigureStorage(builder);
//builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();



void ConfigureAuthentication(WebApplicationBuilder builder)
{

    string key1 = builder.Configuration.GetValue<string>("JwtKey");
    //var key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["JwtKey"];
    var keyBytes = Encoding.ASCII.GetBytes(Globals._KeyT);
    builder.Services.AddAuthentication(configure =>
    {
        configure.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        configure.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(autenticacao =>
    {
        autenticacao.RequireHttpsMetadata = false;
        autenticacao.SaveToken = true;
        autenticacao.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

    builder.Services.AddCors();
    //Globals._KeyT = key1;
}


void ConfigureMVC(WebApplicationBuilder builder)
{
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

}

void ConfigureData(WebApplicationBuilder builder)
{
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseMySql(connString, ServerVersion.AutoDetect(connString));
    });
    //builder.Services.AddTransient<TokenService>();

    
    builder.Services.AddScoped<IUserRepository, IUserRepositoryImpl>();
    builder.Services.AddScoped<IVideoRepository, IVideoRepositoryImpl>();
    builder.Services.AddScoped<ICommentsRepository, ICommentsRepositoryImpl>();
    
}

void ConfigureStorage(WebApplicationBuilder builder)
{
    var connStorageString = builder.Configuration.GetSection("Storage:ConnectionString").Value;
    builder.Services.AddAzureClients(builder =>
    {
        builder.AddBlobServiceClient(connStorageString);
    });

    builder.Services.AddTransient<IStorageService, IStorageServiceImpl>();

}
