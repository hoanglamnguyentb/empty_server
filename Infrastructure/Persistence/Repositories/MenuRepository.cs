using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository(AppDbContext context) : base(context) { }

        public async Task<List<Menu>> GetMenusByPermissionCodesAsync(List<string> permissionCodes)
        {
            return await _dbSet
                .Where(m => permissionCodes.Contains(m.PermissionCode))
                .ToListAsync();
        }
    }
}
