using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    [Table("TB_materials")]
    public class Material
    {
        [Key]
        [MaxLength(50)]
        public string MaterialCode { get; set; } = null!;

        [MaxLength(500)]
        public string MaterialDescription { get; set; } = null!;

        public ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();
        public ICollection<Production> Productions { get; set; } = new List<Production>();
    }
}
