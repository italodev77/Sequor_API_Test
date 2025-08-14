using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    public class Production
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [MaxLength(50)]
        public string OrderId { get; set; } = null!;

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;

        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [MaxLength(50)]
        public string MaterialCode { get; set; } = null!;

        [ForeignKey(nameof(MaterialCode))]
        public Material Material { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal CycleTime { get; set; }
    }
}
