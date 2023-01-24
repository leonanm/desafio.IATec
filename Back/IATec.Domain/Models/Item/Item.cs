using System;
using System.ComponentModel.DataAnnotations;

namespace IATec.Domain.Models.Item
{
    public sealed class Item
    {
        [Key]
        public Guid IdItem { get; set; }
        public string Descricao { get; set; }
    }
}