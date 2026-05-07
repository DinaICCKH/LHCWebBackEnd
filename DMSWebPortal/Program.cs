using System.Text;
using DMSWebPortal.Models;
using DMSWebPortal.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using DMSWebPortal.Hubs;
using Microsoft.AspNetCore.SignalR;
var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Add Controllers & Views
// ----------------------------
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
    });

// --- IMPORTANT: Fix 1252 Encoding Issue ---
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

// Add API Controllers
builder.Services.AddControllers();

// ----------------------------
// Swagger / OpenAPI
// ----------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ICC_Kubera API",
        Version = "v1",
        Description = "This API handles sales login, reporting, and other ICC operations. Developed by Channa.",
        TermsOfService = new Uri("https://yourcompany.com/terms"), // optional
        Contact = new OpenApiContact
        {
            Name = "SAO Channa",
            Email = "saochanna9944@gmail.com",
            Url = new Uri("https://yourcompany.com")
        },
        License = new OpenApiLicense
        {
            Name = "ICCKH License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // JWT Bearer definition
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'"
    });

    // JWT Bearer requirement
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                Name = "Authorization",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

    // Enable annotations for [SwaggerOperation]
    c.EnableAnnotations();

    // Optional: Include XML comments if you want /// <summary> to show in Swagger
    // var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
});

// ----------------------------
// HTTP Client & Token Service
// ----------------------------
builder.Services.AddHttpClient();
builder.Services.AddScoped<JwtTokenService>();









// ----------------------------
// Database
// ----------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ----------------------------
// Session / Cache
// ----------------------------
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Global Exception / Logging Services
builder.Services.AddSingleton<ErrorLogService>();
builder.Services.AddSingleton<RequestLogService>();

// ----------------------------
// JWT Authentication (Access Token ONLY)
// ----------------------------
var accessKey = builder.Configuration["JwtAccessToken:Key"];

if (string.IsNullOrWhiteSpace(accessKey) || accessKey.Length < 32)
    throw new InvalidOperationException("Access Token Key must be at least 32 characters.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(accessKey);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["JwtAccessToken:Issuer"],
            ValidAudience = builder.Configuration["JwtAccessToken:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
        // Custom response for expired/invalid token
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse(); // prevents default redirect
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                var result = System.Text.Json.JsonSerializer.Serialize(new
                {
                    error = "Unauthorized",
                    message = "Access token missing, invalid, or expired"
                });
                return context.Response.WriteAsync(result);
            }
        };
    });

// ----------------------------
// SignalR
// ----------------------------
builder.Services.AddSignalR();

// ----------------------------
// CORS
// ----------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// ----------------------------
// Build app
// ----------------------------
builder.Services.AddSignalR();
var app = builder.Build();



var pathBase = builder.Configuration["PathBase"];
if (!string.IsNullOrEmpty(pathBase))
{
    app.UsePathBase(pathBase);
}

// ----------------------------
// Middleware pipeline
// ----------------------------
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ProgressHub>("/progressHub");
app.UseSession();

// ----------------------------
// Swagger UI
// ----------------------------
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"{pathBase}/swagger/v1/swagger.json", "ICC_Kubera API v1");
    c.RoutePrefix = "swagger"; // e.g., /swagger
});

// ----------------------------
// MVC Default Route
// ----------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DMS}/{action=Login}/{id?}");

// ----------------------------
// API Controllers
// ----------------------------
app.MapControllers();

// ----------------------------
// Redirect root to Login
// ----------------------------
app.MapGet("/", (HttpContext context) =>
{
    var baseUrl = UrlHelper.GetBaseUrl(context);
    context.Response.Redirect($"{baseUrl}/dms/login");
    return Task.CompletedTask;
});

// ----------------------------
// SignalR Hub (Optional)
// ----------------------------
// app.MapHub<SignalRChat.Hubs.ChatHub>("/chatHub");

// ----------------------------
// Global Exception Middleware
// ----------------------------
app.UseMiddleware<GlobalExceptionMiddleware>();

app.Run();