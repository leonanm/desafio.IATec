using System;
using System.Threading.Tasks;
using IATec.Domain.Models;
using IATec.Domain.Models.Pedido;

namespace IATec.Domain.Interfaces.Services
{
    public interface IVendaService
    {
        public Task<ApiResult> Salvar(Venda venda);
        public Task<ApiResult> Buscar(Guid idVenda);
        public Task<ApiResult> Alterar(Venda venda);

    }
}