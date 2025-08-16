using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    [Table("product_materials")]
    public class ProductMaterial
    {
        [Key, Column(Order = 0)]
        public string ProductCode { get; set; } = String.Empty;
        [ForeignKey(nameof(ProductCode))]
        public Product Product { get; set; }
        [Key, Column(Order = 1)]
        public string MaterialCode { get; set; } = String.Empty;
        [ForeignKey(nameof(MaterialCode))]
        public Material Material { get; set; }
    }
}
