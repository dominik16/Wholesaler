using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Wholesaler;
using Wholesaler.Data;
using Wholesaler.DataTransferObject;
using Wholesaler.DataTransferObject.Validators;
using Wholesaler.Middelware;
using Wholesaler.Models;
using Wholesaler.Services;

var builder = WebApplication.CreateBuilder(args);

var authSettings = new AuthSettings();
builder.Configuration.GetSection("Authentication").Bind(authSettings);

builder.Services.AddSingleton(authSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authSettings.JwtIssuer,
        ValidAudience = authSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.JwtKey))
    };
});

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(options => options.LocalizationEnabled = false);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Wholesaler-API", Version = "v1" });
    option.OperationFilter<AddSwaggerService>();
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Description = "Please insert JWT token (just token, without Bearer!) into field"
    });
});

//Nlog
builder.WebHost.UseNLog();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IManageService, ManageService>();

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<CreateUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<ProductQuery>, ProductQueryValidator>();
builder.Services.AddScoped<WholesalerSeeder>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", policy =>
    {
        policy.AllowAnyMethod().AllowAnyHeader().WithOrigins(builder.Configuration["AllowedOrigins"]);
    });
});

var app = builder.Build();

app.UseCors("FrontEndClient");

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<WholesalerSeeder>();
seeder.Seed();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
