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
        [MaxLength(50)]
        public string ProductCode { get; set; } = null!;

        [ForeignKey(nameof(ProductCode))]
        public Product Product { get; set; } = null!;

        [Key, Column(Order = 1)]
        [MaxLength(50)]
        public string MaterialCode { get; set; } = null!;

        [ForeignKey(nameof(MaterialCode))]
        public Material Material { get; set; } = null!;
    }
}
