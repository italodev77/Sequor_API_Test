using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    [Table("materials")]
    public class Material
    {
        [Key]
        public string MaterialCode { get; set; } = String.Empty;
        public string MaterialDescription { get; set; } = String.Empty;

        public ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();
        public ICollection<Production> Productions { get; set; } = new List<Production>();
    }
}
