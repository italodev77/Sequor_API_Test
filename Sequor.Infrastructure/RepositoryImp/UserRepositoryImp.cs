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
    public class UserRepositoryImp : IUserRepository
    {
        private readonly SequorDbContext _context;

        public UserRepositoryImp(SequorDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
