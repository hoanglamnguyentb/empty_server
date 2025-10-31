using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
