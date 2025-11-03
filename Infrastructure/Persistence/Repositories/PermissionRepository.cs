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
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext context) : base(context) { }

        public async Task<List<Permission>> GetByRoleIdsAsync(List<string> roleIds)
        {
            var rolePermissions = await _context.RolePermissions
                .Where(rp => roleIds.Contains(rp.RoleId))
                .ToListAsync();

            var permissionIds = rolePermissions.Select(rp => rp.PermissionId).ToList();

            return await _dbSet.Where(p => permissionIds.Contains(p.Id)).ToListAsync();
        }
    }
}
