using Sequor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Infrastructure.IRepositories
{
    public interface IProductionRepository
    {
        Task<List<Production>> GetByEmailAsync(string email);
        Task AddAsync(Production production);   
    }
}
