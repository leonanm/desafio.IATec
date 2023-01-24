using System;
using System.Threading.Tasks;
using IATec.Domain.Interfaces.Services;
using IATec.Domain.Models.Pedido;
using Microsoft.AspNetCore.Mvc;

namespace IATec.Api.Controllers
{
    [Route("v1/vendas")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly IVendaService _vendaService;

        public VendaController(IVendaService vendaService)
        {
            _vendaService = vendaService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Venda pedido)
        {
            var apiResult = await _vendaService.Salvar(pedido);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

        [HttpGet("{idVenda}")]

        public async Task<IActionResult> Get(Guid idVenda)
        {
            var apiResult = await _vendaService.Buscar(idVenda);
            return StatusCode(apiResult.StatusCode, apiResult);
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Venda pedido)
        {
            var apiResult = await _vendaService.Alterar(pedido);
            return StatusCode(apiResult.StatusCode, apiResult);
        }

    }
}
