using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServerBlazor.Data;
using ServerBlazor.Models;
using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.ResponseCompression;
using ServerBlazor.Hubs;
using ServerBlazor;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "My API",
        Version = "v1",
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:44338/");
});
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddTransient<IBlogRepository, BlogRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<SignalRService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddSignInManager<SignInManager<IdentityUser>>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
// st�tte for JWT
var confKey = builder.Configuration.GetSection("TokenSettings")["SecretKey"];
var key = Encoding.ASCII.GetBytes(confKey);
//builder.Services.AddAuthentication()
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //.AddCookie(cfg => cfg.SlidingExpiration = true)
    .AddJwtBearer(x =>
    {
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
    //x.Events = new JwtBearerEvents
    //{
    //    OnMessageReceived = context =>
    //    {
    //        var accessToken = context.Request.Query["access_token"];

    //        // If the request is for our hub...
    //        var path = context.HttpContext.Request.Path;
    //        if (!string.IsNullOrEmpty(accessToken) &&
    //            path.StartsWithSegments("/notihub"))
    //        {
    //            // Read the token out of the query string
    //            context.Token = accessToken;
    //        }
    //        return Task.CompletedTask;
    //    }
    //};
});
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
});

var app = builder.Build();
app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapBlazorHub();
    app.MapHub<ChatHub>("/chathub");
    app.MapHub<NotiHub>("/notihub");
    endpoints.MapRazorPages();
    endpoints.MapFallbackToPage("/_Host");
});

app.Run();
