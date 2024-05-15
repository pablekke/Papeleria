using Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AccesoADatos
{
    public class Contexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoComun> PedidosComunes { get; set; }
        public DbSet<PedidoExpres> PedidosExpres { get; set; }
        public DbSet<LineaPedido> LineasPedido { get; set; }
        public Contexto(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(u => u.UsuarioId);
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Cliente>().HasKey(u => u.ClienteId);
            modelBuilder.Entity<Cliente>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Cliente>().OwnsOne(c => c.Direccion, direccion =>
            {
                direccion.Property(d => d.Ciudad).HasColumnName("Ciudad");
                direccion.Property(d => d.Calle).HasColumnName("Calle");
                direccion.Property(d => d.NumeroPuerta).HasColumnName("NumeroPuerta");
            });

            modelBuilder.Entity<Articulo>().HasKey(a => a.ArticuloId);
            modelBuilder.Entity<Articulo>().HasIndex(a => a.Codigo).IsUnique();
            modelBuilder.Entity<Articulo>().HasIndex(a => a.Nombre).IsUnique();

            modelBuilder.Entity<Pedido>().HasKey(a => a.PedidoId);
            modelBuilder.Entity<Pedido>().HasOne(a => a.Cliente) //un pedido tiene un cliente
                                         .WithMany() //un cliente tiene varios pedidos
                                         .HasForeignKey(a => a.ClienteId); //la foreign key es la del cliente

            modelBuilder.Entity<PedidoComun>().ToTable("PedidosComunes");

            modelBuilder.Entity<PedidoExpres>().ToTable("PedidosExpres");

            modelBuilder.Entity<LineaPedido>().HasKey(lp => lp.LineaPedidoId);

            modelBuilder.Entity<LineaPedido>()
                .HasOne<Pedido>() //la entidad tiene un pedido
                .WithMany(p => p.Lineas) //un pedido tiene muchas lineas
                .HasForeignKey(lp => lp.PedidoId); //la entidad tiene una referencia a el pedido

            modelBuilder.Entity<LineaPedido>()
                .HasOne(lp => lp.Articulo) //cada entidad tiene un articulo
                .WithMany() //un articulo puede estar en varias lineas
                .HasForeignKey(lp => lp.ArticuloId); //cada entidad tiene una fk de articulo

            base.OnModelCreating(modelBuilder);
        }
    }
}
