using System.Data;
using EasyCheck.Contexts;
using EasyCheck.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IRfidReaderService, RfidReaderService>();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<AttendanceService>();

builder.Services.AddTransient<IDbConnection>((sp) => new MySqlConnection(builder.Configuration.GetConnectionString("MySqlConnection")));
builder.Services.AddTransient<StudentService>();
builder.Services.AddTransient<TeacherService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => { options.LoginPath = "/login"; });

builder.Services.AddAuthorization();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
