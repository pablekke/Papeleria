using Microsoft.EntityFrameworkCore;
using Servicios;
using AccesoADatos;

namespace ComercioMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Conexiones
            builder.Services.AddDbContext<DbContext, Contexto>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("StringConnection"));
            });
            #endregion

            #region Inyecciones
            //inyeccion de dependencias
            builder.Services.AddScoped(typeof(IServicioUsuario), typeof(ServicioUsuario));
            builder.Services.AddScoped(typeof(IRepositorioUsuario), typeof(RepositorioUsuario));

            builder.Services.AddScoped(typeof(IServicioCliente), typeof(ServicioCliente));
            builder.Services.AddScoped(typeof(IRepositorioCliente), typeof(RepositorioCliente));

            builder.Services.AddScoped(typeof(IServicioArticulo), typeof(ServicioArticulo));
            builder.Services.AddScoped(typeof(IRepositorioArticulo), typeof(RepositorioArticulo));

            builder.Services.AddScoped(typeof(IServicioPedido), typeof(ServicioPedido));
            builder.Services.AddScoped(typeof(IRepositorioPedido), typeof(RepositorioPedido));

            builder.Services.AddScoped(typeof(IServicioPedidoComun), typeof(ServicioPedidoComun));
            builder.Services.AddScoped(typeof(IRepositorioPedidoComun), typeof(RepositorioPedidoComun));

            builder.Services.AddScoped(typeof(IServicioPedidoExpres), typeof(ServicioPedidoExpres));
            builder.Services.AddScoped(typeof(IRepositorioPedidoExpres), typeof(RepositorioPedidoExpres));

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion

            #region Sesión
            builder.Services.AddDistributedMemoryCache(); // Usar memoria como back store de caché

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(300); // Tiempo de expiración de la sesión
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            #endregion

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Cuenta}/{action=LogIn}/{id?}");

            app.Run();
        }
    }
}