using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    public class Material
    {
        public string MaterialCode { get; set; } = null!;
        public string MaterialDescription { get; set; } = null!;
        public ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();
    }
}
