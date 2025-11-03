using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RolePermission : BaseEntity
    {
        public string? RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}
