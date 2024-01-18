using BlazorApp.Server.DataBase;
using BlazorApp.Server.DataBase.Repository;
using BlazorApp.Server.DataBase.Repository.Interfaces;
using BlazorApp.Server.Services;
using BlazorApp.Server.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(config =>
    config.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IWindowsRepository, WindowsRepository>();
builder.Services.AddScoped<ISubElementsRepository, SubElementsRepository>();

builder.Services.AddScoped<IProjectOrdersService, OrdersService>();
builder.Services.AddScoped<IProjectWindowsService, WindowsService>();
builder.Services.AddScoped<IProjectSubElementsService, SubElementsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.MapFallbackToFile("index.html");

app.Run();
