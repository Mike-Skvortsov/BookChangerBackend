using BusinessLogic;
using Microsoft.AspNetCore.SignalR;
using Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using BusinessLogic.AutoMapperProfile;
using BooksChanger;
using BooksChanger.Hubs;
using System.Net.Sockets;
using System.Security.Policy;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache(); // Add this line
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddBusinessLogicServices();
builder.Services.AddAutoMapper(typeof(UserProfile), typeof(BookProfile), typeof(AuthorProfile), typeof(GenreProfile), typeof(GenreProfile));
builder.Services.AddRepositoryServices();
var connectionString = builder.Configuration.GetConnectionString("DB");
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));
builder.Services.AddSignalR();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
    policy =>
    {
        policy.WithOrigins("https://book-changer.vercel.app")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .WithExposedHeaders("X-Total-Pages"); // Expose X-Total-Pages header
    }));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<Context>();
    dbContext.Database.Migrate();
}

app.MapHub<ChatHub>("/chathub");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseCors("NgOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
