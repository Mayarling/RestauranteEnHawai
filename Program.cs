using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute("Plato", "{controller=Plato}/{action=Index}/{id?}");
app.MapControllerRoute("Venta", "{controller=Venta}/{action=Index}/{numeroOrden?}");
app.MapControllerRoute("Reserva", "{controller=Reserva}/{action=Index}/{numeroReserva?}");
app.MapControllerRoute("Cliente", "{controller=Cliente}/{action=Index}/{id?}");
app.MapControllerRoute("Reportes", "{controller=Reportes}/{action=Index}");

app.Run();
