using IATec.Domain.Models.Pedido;
using System.Threading.Tasks;
using System;

namespace IATec.Domain.Interfaces.Repositories
{
    public interface IVendaRepository
    {
        public Task Salvar(Venda venda);
        public Task<Venda> Buscar(Guid idVenda);
        public Task Alterar(Venda venda);

    }
}