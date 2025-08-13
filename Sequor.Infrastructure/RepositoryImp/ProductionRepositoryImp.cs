using Microsoft.EntityFrameworkCore;
using Sequor.Domain.Entities;
using Sequor.Infrastructure.Data;
using Sequor.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Infrastructure.RepositoryImp
{
    public class ProductionRepository : IProductionRepository
    {
        private readonly SequorDbContext _context;

        public ProductionRepository(SequorDbContext context)
        {
            _context = context;
        }

        public async Task<List<Production>> GetByEmailAsync(string email)
        {
            return await _context.Productions
                .Where(p => p.Email == email)
                .ToListAsync();
        }

        public async Task AddAsync(Production production)
        {
            _context.Productions.Add(production);
            await _context.SaveChangesAsync();
        }
    }
}
