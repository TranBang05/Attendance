using AttendanceFe.Controllers;
using AttendanceFe.Models;
using AttendanceFe.Service;
using AttendanceFe.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Th?i gian timeout c?a Session
    options.Cookie.HttpOnly = true; // Ch? có HTTP request m?i có th? truy c?p Cookie
    options.Cookie.IsEssential = true; // Cookie là b?t bu?c ?? ho?t ??ng c?a ?ng d?ng
});

builder.Services.AddHttpClient("WithAuth", client =>
{
    client.BaseAddress = new Uri("http://localhost:5095/api/");
}).AddHttpMessageHandler<TokenHandler>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddTransient<TokenHandler>();
builder.Services.AddTransient<HomeController>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<HomeController>>();
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    return new HomeController(logger, httpClientFactory);
});

builder.Services.AddGrpcClient< SubjectRPC.SubjectRPCClient>(o =>
{
    o.Address = new Uri("http://localhost:5095");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});

builder.Services.AddScoped<SubjectService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
