using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;

// 1. KHỞI TẠO BUILDER TRƯỚC TIÊN
var builder = WebApplication.CreateBuilder(args);


// 2. SAU ĐÓ MỚI SỬ DỤNG BUILDER ĐỂ CẤU HÌNH
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SupermarketDbContext>(options =>
    options.UseSqlServer(connectionString));

// Cấu hình JWT
var jwtSecret = builder.Configuration["JwtSettings:Secret"]
    ?? "SupermarketSecretKeyDoAnMonHoc2026SecureString!!";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(jwtSecret)
        ),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ⭐ Cấu hình Swagger JWT
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập JWT token theo dạng: Bearer {token}"
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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();