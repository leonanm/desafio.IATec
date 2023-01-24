using IATec.Domain.Models.Item;
using IATec.Domain.Models.Pedido;
using IATec.Domain.Models.Vendedor;
using Microsoft.EntityFrameworkCore;

namespace IATec.Infra.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Item> Itens { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
    }
}