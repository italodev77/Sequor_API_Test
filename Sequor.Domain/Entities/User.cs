using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.Entities
{
    public class User
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
