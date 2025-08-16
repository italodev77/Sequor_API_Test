using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    [Table("productions")]
    public class Production
    {
        [Key]
        public long Id { get; set; }
        public string Email { get; set; } = String.Empty;
        public string OrderId { get; set; } = String.Empty;

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }
        public string MaterialCode { get; set; } = String.Empty;

        [ForeignKey(nameof(MaterialCode))]
        public Material Material { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CycleTime { get; set; }
    }
}
