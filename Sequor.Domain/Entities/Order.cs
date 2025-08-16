using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    [Table("order")]
    public class Order
    {
        [Key]
        public string OrderId { get; set; } = String.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }
        [ForeignKey(nameof(Product))]
        public string ProductCode { get; set; } = String.Empty;
        public Product Product { get; set; }

        public ICollection<Production> Productions { get; set; } = new List<Production>();
    }
}
