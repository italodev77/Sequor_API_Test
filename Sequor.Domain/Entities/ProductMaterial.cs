using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    public class ProductMaterial
    {
        public string ProductCode { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public string MaterialCode { get; set; } = null!;
        public Material Material { get; set; } = null!;
    }
}
