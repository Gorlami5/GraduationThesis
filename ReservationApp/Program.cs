using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReservationApp.BusinessUnit;
using ReservationApp.BusinessUnit.Interfaces;
using ReservationApp.Context;
using ReservationApp.DataAccessUnit;
using ReservationApp.DataAccessUnit.Interfaces;
using ReservationApp.Extensions;
using System.Text;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration.GetSection("JWTSettings:Token").Value;
var ekey = Encoding.ASCII.GetBytes(key);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; // Newtonsoft referenceloophandling configuration
});
builder.Services.AddScoped<IAuthBusinessUnit, AuthBusinessUnit>();
builder.Services.AddScoped<ICityBusinessUnit, CityBusinessUnit>();
builder.Services.AddScoped<IPhotoBusinessUnit, PhotoBusinessUnit>();
builder.Services.AddScoped<IAuthDataAccess, AuthDataAccess>();
builder.Services.AddScoped<ICityDataAccess, CityDataAccess>();
builder.Services.AddScoped<IPhotoDataAccess, PhotoDataAccess>();
builder.Services.AddScoped<IReservationDataAccess, ReservationDataAccess>();
builder.Services.AddScoped<IReservationBusinessUnit, ReservationBusinessUnit>();
builder.Services.AddDbContext<PostgreDbConnection>(options=> options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<CloudinaryInformation>(builder.Configuration.GetSection("CloudinaryInformation"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(ekey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
