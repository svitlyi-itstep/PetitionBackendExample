using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PetitionBackend.DbContexts;
using PetitionBackend.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ▶ Налаштування роботи з JWT Bearer
// https://learn.microsoft.com/uk-ua/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-10.0
builder.Services.AddAuthentication();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                ),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// ▶ Додавання контексту бази даних у проєкт
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // ▶ Підключення бази даних через Connection String
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ▶ Підключення сервісів у проєкт
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();

// ▶ Налаштування CORS для роботи з React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// ▶ Налаштування роботи з JWT-токенами у Swagger
// Тут можна знайти інші опції налаштування Swagger:
// https://learn.microsoft.com/uk-ua/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&viewFallbackFrom=aspnetcore-10.0&tabs=visual-studio
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введіть JWT токен"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<String>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ▶ Підключення CORS
app.UseCors("AllowReact");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
