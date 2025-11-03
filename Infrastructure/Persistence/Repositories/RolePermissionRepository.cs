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
    public class RolePermissionRepository : Repository<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(AppDbContext context) : base(context) { }

        public async Task<List<RolePermission>> GetByRoleIdsAsync(List<string> roleIds)
        {
            return await _dbSet
                .Where(rp => roleIds.Contains(rp.RoleId))
                .ToListAsync();
        }
    }
}
