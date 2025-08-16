using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    [Table("products")]
    public class Product
    {
        [Key]
        public string ProductCode { get; set; } = String.Empty;
        public string ProductDescription { get; set; } = String.Empty;
        public string? Image { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CycleTime { get; set; }

        public ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
