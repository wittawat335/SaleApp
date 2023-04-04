using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Sales.IOC;
using Sales.Utility.Common;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-------------------Add DBContext from IOC by InjectDependence -----------------//
builder.Services.InjectDependence(builder.Configuration);

//-------------------------- JWT -------------------------------------------//
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection(Constants.AppSettings.JWT_Key).Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

string url = builder.Configuration.GetSection(Constants.AppSettings.Client_URL).Value!;
//-------------------------- Add cor ----------------------------/ 
builder.Services.AddCors(options =>
{
    options.AddPolicy("newPolicy", app =>
    {
        app.WithOrigins(url).AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("newPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
