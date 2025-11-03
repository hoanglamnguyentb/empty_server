using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PermissionService
    {
        private readonly IPermissionRepository _permissionRepo;
        private readonly IRolePermissionRepository _rolePermissionRepo;

        public PermissionService(
            IPermissionRepository permissionRepo,
            IRolePermissionRepository rolePermissionRepo)
        {
            _permissionRepo = permissionRepo;
            _rolePermissionRepo = rolePermissionRepo;
        }

        public async Task<List<Permission>> GetPermissionsByRoles(List<string> roleIds)
        {
            var rolePermissions = await _rolePermissionRepo.GetByRoleIdsAsync(roleIds);

            var permissionIds = rolePermissions.Select(rp => rp.PermissionId).ToList();

            var permissions = await _permissionRepo.GetAllAsync();

            return permissions.Where(p => permissionIds.Contains(p.Id)).ToList();
        }
    }
}
