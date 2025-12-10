using Microsoft.EntityFrameworkCore;
using SMApp.Components;
using SMApp.Data;
using SMApp.Services;
using SMApp.Services.Contracts;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSyncfusionBlazor();

builder.Services.AddDbContext<SalesManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped<IEmployeeManagementService, EmployeeManagementService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ISalesOrderReportService, SalesOrderReportService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

SyncfusionLicenseProvider.RegisterLicense(builder.Configuration["ApiSettings:SyncfusionLicenseKey"]!);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
