using System;
using System.Linq;
using System.Threading.Tasks;
using IATec.Domain.Interfaces.Repositories;
using IATec.Domain.Models.Pedido;
using IATec.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace IATec.Infra.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly DatabaseContext _context;

        public VendaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task Salvar(Venda venda)
        {
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();
        }

        public async Task<Venda> Buscar(Guid idVenda)
        {
            return await _context.Vendas.Where(_ => _.IdVenda == idVenda)
                .Include(_=> _.Vendedor)
                .Include(_=> _.Itens)
                .FirstOrDefaultAsync();
        }

        public async Task Alterar(Venda venda)
        {
            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();
        }
    }
}