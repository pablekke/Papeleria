using AccesoADatos;
using Microsoft.EntityFrameworkCore;
using Servicios;

namespace WebAPI
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
            builder.Services.AddScoped(typeof(IServicioArticulo), typeof(ServicioArticulo));
            builder.Services.AddScoped(typeof(IRepositorioArticulo), typeof(RepositorioArticulo));
            builder.Services.AddScoped(typeof(IServicioPedido), typeof(ServicioPedido));
            builder.Services.AddScoped(typeof(IRepositorioPedido), typeof(RepositorioPedido));

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
