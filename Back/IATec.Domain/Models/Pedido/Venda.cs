using IATec.Domain.Enum;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace IATec.Domain.Models.Pedido
{
    public sealed class Venda
    {
        [Key]
        public Guid IdVenda { get; set; }
        public Vendedor.Vendedor Vendedor { get; set; }
        public DateTime DataVenda { get; set; }
        public List<Item.Item> Itens { get; set; }
        public StatusVenda Status { get; set; }
    }
}