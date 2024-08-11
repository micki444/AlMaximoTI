using AlMaximoTI.Models;
using AlMaximoTI.Repositorios.Contrato;
using AlMaximoTI.Repositorios.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGenericRepository<ProductoProveedor>, ProductoProveedorRepository>();
builder.Services.AddScoped<IGenericRepository<Producto>, ProductoRepository>();
builder.Services.AddScoped<IGenericRepository<TipoProducto>, TipoProductoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
