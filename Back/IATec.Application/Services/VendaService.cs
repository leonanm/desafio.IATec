using System;
using System.Linq;
using System.Threading.Tasks;
using IATec.Domain.Enum;
using IATec.Domain.Interfaces.Repositories;
using IATec.Domain.Interfaces.Services;
using IATec.Domain.Models;
using IATec.Domain.Models.Pedido;

namespace IATec.Application.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;

        public VendaService(IVendaRepository vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }

        public async Task<ApiResult> Salvar(Venda venda)
        {
            try
            {
                if (!venda.Itens.Any())
                {
                    return new ApiResult
                    {
                        Message = "A Venda deve possuir no minimo um item",
                        StatusCode = 400
                    };
                }

                venda.Status = StatusVenda.AguardandoPagamento;
                venda.DataVenda = DateTime.Now;

                await _vendaRepository.Salvar(venda);

                return new ApiResult
                {
                    Message = "Venda Processada com Sucesso!",
                    StatusCode = 201,
                    Result = venda
                };
            }
            catch (Exception e)
            {
                return new ApiResult
                {
                    Message = "Erro para salvar o pedido",
                    StatusCode = 500
                };
            }
        }

        public async Task<ApiResult> Buscar(Guid idVenda)
        {
            try
            {
                if (idVenda == Guid.Empty)
                {
                    return new ApiResult
                    {
                        Message = "Id do Pedido invalido",
                        StatusCode = 400
                    };
                }

                var pedido = await _vendaRepository.Buscar(idVenda);

                if (pedido != null)
                {
                    return new ApiResult
                    {
                        Result = pedido,
                        StatusCode = 200
                    };

                }

                return new ApiResult
                {
                    Message = "Pedido não encontrado!",
                    StatusCode = 404
                };
            }
            catch (Exception e)
            {
                return new ApiResult
                {
                    Message = "Erro para buscar o pedido",
                    StatusCode = 500
                };
            }
        }

        public async Task<ApiResult> Alterar(Venda venda)
        {
            try
            {
                if (venda.Status == StatusVenda.AguardandoPagamento)
                {
                    return new ApiResult
                    {
                        Message = "Venda aguardando pagamento",
                        StatusCode = 500    
                    };
                }

                if (venda.IdVenda == Guid.Empty)
                {
                    return new ApiResult
                    {
                        Message = "Pedido invalido",
                        StatusCode = 400
                    };
                }

                var resultVenda = await _vendaRepository.Buscar(venda.IdVenda);

                if (resultVenda == null)
                {
                    return new ApiResult
                    {
                        Message = "Pedido não encontrado!",
                        StatusCode = 404
                    };
                }

                var validarStatusVenda = ValidarStatus(resultVenda.Status, venda);


                if (validarStatusVenda is false)
                    return new ApiResult
                    {
                        Message = "Status de venda não permitido",
                        StatusCode = 500
                    };


                await _vendaRepository.Alterar(venda);

                return new ApiResult
                {
                    Result = venda,
                    Message = "Venda alterada com sucesso",
                    StatusCode = 200
                };
            }
            catch (Exception e)
            {
                return new ApiResult
                {
                    Message = "Erro para alterar o pedido",
                    StatusCode = 500
                };
            }
        }



        private bool ValidarStatus(StatusVenda statusVenda, Venda venda)
        {
            switch (statusVenda)
            {
                case StatusVenda.AguardandoPagamento when (venda.Status == StatusVenda.PagamentoAprovado || venda.Status == StatusVenda.Cancelado):
                case StatusVenda.PagamentoAprovado when (venda.Status == StatusVenda.Enviado || venda.Status == StatusVenda.Cancelado):
                case StatusVenda.Enviado when venda.Status == StatusVenda.Entregue:
                    return true;
                default:
                    return false;
            }
        }
    }
}