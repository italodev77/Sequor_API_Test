using Microsoft.EntityFrameworkCore;
using Sequor.Application.IRepositories;
using Sequor.Domain.Entities;
using Sequor.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Infrastructure.RepositoryImp
{
    public class OrderRepositoryImp : IOrderRepository
    {
        private readonly SequorDbContext _context;

        public OrderRepositoryImp(SequorDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Product)
                    .ThenInclude(p => p.ProductMaterials)
                        .ThenInclude(pm => pm.Material)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(string orderId)
        {
            return await _context.Orders
                .Include(o => o.Product)
                    .ThenInclude(p => p.ProductMaterials)
                        .ThenInclude(pm => pm.Material)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
    }
}
