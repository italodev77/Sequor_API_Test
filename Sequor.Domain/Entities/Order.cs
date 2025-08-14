using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    [Table("TB_orders")]
    public class Order
    {
        [Key]
        [MaxLength(50)]
        public string OrderId { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [MaxLength(50)]
        public string ProductCode { get; set; } = null!;

        [ForeignKey(nameof(ProductCode))]
        public Product Product { get; set; } = null!;

        public ICollection<Production> Productions { get; set; } = new List<Production>();
    }
}
