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
        public string Email { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
