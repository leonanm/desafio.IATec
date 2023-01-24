using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeItEasy;
using IATec.Domain.Enum;
using IATec.Domain.Interfaces.Services;
using IATec.Domain.Models;
using IATec.Domain.Models.Item;
using IATec.Domain.Models.Pedido;
using Moq;
using NUnit.Framework;

namespace IATec.Tests.Services
{
    [TestFixture]
    public class VendaServiceTests
    {
        [Test]
        public async Task Deve_Retornar_Status_200_Ao_Salvar_Uma_Venda()
        {
            var vendaServiceMock = new Mock<IVendaService>();

            var venda = new Venda
            {
                Status = StatusVenda.AguardandoPagamento,
                DataVenda = DateTime.Now,
                IdVenda = Guid.NewGuid(),
                Itens = new List<Item>
                {
                    new Item
                    {
                        Descricao = "bala",
                        IdItem = Guid.NewGuid()
                    }
                }
            };

            vendaServiceMock.Setup(i => i.Salvar(It.IsAny<Venda>())).ReturnsAsync(new ApiResult
            {
                StatusCode = 200
            });

            var apiResult = await vendaServiceMock.Object.Salvar(venda);

            Assert.That(200, Is.EqualTo(apiResult.StatusCode));
        }


        [Test]
        public async Task Deve_Retornar_Status_400_Ao_Salvar_Uma_Venda()
        {
            var vendaServiceMock = new Mock<IVendaService>();

            var venda = new Venda
            {
                Status = StatusVenda.AguardandoPagamento,
                DataVenda = DateTime.Now,
                IdVenda = Guid.NewGuid(),
                Itens = new List<Item>()
            };

            vendaServiceMock.Setup(i => i.Salvar(It.IsAny<Venda>())).ReturnsAsync(new ApiResult
            {
                StatusCode = 400
            });

            var apiResult = await vendaServiceMock.Object.Salvar(venda);

            Assert.That(400, Is.EqualTo(apiResult.StatusCode));
        }


        [Test]
        public async Task Deve_Retornar_Status_500_Ao_Validar_Se_A_Pelo_Menos_Um_Item_Na_Venda()
        {
            var vendaServiceMock = new Mock<IVendaService>();

            var venda = new Venda
            {
                Status = StatusVenda.AguardandoPagamento,
                DataVenda = DateTime.Now,
                IdVenda = Guid.NewGuid(),
                Itens = new List<Item>()
            };

            vendaServiceMock.Setup(i => i.Salvar(It.IsAny<Venda>())).ReturnsAsync(new ApiResult
            {
                StatusCode = 500,
                Message = "A Venda deve possuir no minimo um item"
            });

            var apiResult = await vendaServiceMock.Object.Salvar(venda);

            Assert.That("A Venda deve possuir no minimo um item", Is.EqualTo(apiResult.Message));
        }


        [Test]
        public async Task Deve_Validar_Se_Ao_Salvar_O_Status_Esta_AguardandoPagamento()
        {
            var vendaServiceMock = new Mock<IVendaService>();

            var venda = new Venda
            {
                Status = StatusVenda.AguardandoPagamento,
                DataVenda = DateTime.Now,
                IdVenda = Guid.NewGuid(),
                Itens = new List<Item>()
            };

            vendaServiceMock.Setup(i => i.Salvar(It.IsAny<Venda>())).ReturnsAsync(new ApiResult
            {
                StatusCode = 200,
                Result = venda
            });

            var apiResult = await vendaServiceMock.Object.Salvar(venda);


            Venda status = (Venda)apiResult.Result;

            Assert.That(StatusVenda.AguardandoPagamento, Is.EqualTo(status.Status));

        }

        [Test]
        public async Task Deve_Retornar_Status_500_Ao_Salvar_O_Pedido()
        {
            var vendaServiceMock = new Mock<IVendaService>();

            var venda = new Venda
            {
                Status = StatusVenda.AguardandoPagamento,
                DataVenda = DateTime.Now,
                IdVenda = Guid.NewGuid(),
                Itens = new List<Item>()
            };

            vendaServiceMock.Setup(i => i.Salvar(It.IsAny<Venda>())).ReturnsAsync(new ApiResult
            {
                StatusCode = 500
            });

            var apiResult = await vendaServiceMock.Object.Salvar(venda);

            Assert.That(500, Is.EqualTo(apiResult.StatusCode));
        }

        [Test]

        public async Task Deve_Retornar_Status_500_Apos_Alterar_A_venda()
        {
            var vendaServiceMock = new Mock<IVendaService>();

            var venda = new Venda
            {
                Status = StatusVenda.AguardandoPagamento,
                DataVenda = DateTime.Now,
                IdVenda = Guid.NewGuid(),
                Itens = new List<Item>()
            };

            vendaServiceMock.Setup(i => i.Alterar(It.IsAny<Venda>())).ReturnsAsync(new ApiResult
            {
                StatusCode = 500,
                Message = "Venda aguardando pagamento"
            });

            var apiResult = await vendaServiceMock.Object.Alterar(venda);

            Assert.That(500, Is.EqualTo(apiResult.StatusCode));
        }


        [Test]
        public async Task Deve_Retornar_Status_400_Apos_Alterar_E_O_Pedido_For_Invalido()
        {
            var vendaServiceMock = new Mock<IVendaService>();

            var venda = new Venda
            {
                Status = StatusVenda.AguardandoPagamento,
                DataVenda = DateTime.Now,
                IdVenda = Guid.NewGuid(),
                Itens = new List<Item>()
            };

            vendaServiceMock.Setup(i => i.Alterar(It.IsAny<Venda>())).ReturnsAsync(new ApiResult
            {
                StatusCode = 400,
                Message = "Pedido invalido"
            });

            var apiResult = await vendaServiceMock.Object.Alterar(venda);

            Assert.That(400, Is.EqualTo(apiResult.StatusCode));
        }

        [Test]
        public async Task Deve_Validar_O_Status_Apos_Alterar_a_Venda()
        {
            var vendaServiceMock = new Mock<IVendaService>();

            var venda = new Venda
            {
                Status = StatusVenda.AguardandoPagamento,
                DataVenda = DateTime.Now,
                IdVenda = Guid.NewGuid(),
                Itens = new List<Item>()
            };

            vendaServiceMock.Setup(i => i.Alterar(It.IsAny<Venda>())).ReturnsAsync(new ApiResult
            {
                StatusCode = 200,
                Message = "Venda alterada com sucesso",
                Result = venda
            });

            var apiResult = await vendaServiceMock.Object.Alterar(venda);


            Venda status = (Venda)apiResult.Result;

            Assert.That(StatusVenda.AguardandoPagamento, Is.EqualTo(status.Status));
        }

        [Test]
        public async Task Deve_Retornar_o_Status_400_Se_o_ID_For_Invalido()
        {
            var vendaServiceMock = new Mock<IVendaService>();


            vendaServiceMock.Setup(i => i.Buscar(Guid.Empty)).ReturnsAsync(new ApiResult
            {
                Message = "Id do Pedido invalido",
                StatusCode = 400
            });
            var apiResult = await vendaServiceMock.Object.Buscar(Guid.Empty);


            Venda status = (Venda)apiResult.Result;

            Assert.That(400, Is.EqualTo(apiResult.StatusCode));
        }
    }
    }
    