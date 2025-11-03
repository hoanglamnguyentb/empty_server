using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string? Label { get; set; }
        public string? Path { get; set; }
        public string? PermissionCode { get; set; }
    }

}
