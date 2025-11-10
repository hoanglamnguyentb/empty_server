using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CreateMenuRequest
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Icon { get; set; }
        public string? Url { get; set; }
        public int? ParentId { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateMenuRequest : CreateMenuRequest
    {
        public int Id { get; set; }
    }
}
