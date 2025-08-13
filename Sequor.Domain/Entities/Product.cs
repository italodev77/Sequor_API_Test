using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    public class Product
    {
        public string ProductCode { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public string? Image { get; set; }
        public decimal CycleTime { get; set; }
        public ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();
    }
}
