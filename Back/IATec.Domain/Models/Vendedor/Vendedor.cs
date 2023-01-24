using System;
using System.ComponentModel.DataAnnotations;

namespace IATec.Domain.Models.Vendedor
{
    public sealed class Vendedor
    {
        [Key]
        public Guid IdVendedor { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }
    }
}