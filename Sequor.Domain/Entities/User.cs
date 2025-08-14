using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
