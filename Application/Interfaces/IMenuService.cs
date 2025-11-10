using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMenuService
    {
        Task<List<Menu>> GetAllAsync();
        Task<List<Menu>> GetTreeAsync();
        Task<Menu?> GetByIdAsync(int id);
        Task<Menu> CreateAsync(Menu menu);
        Task<Menu> UpdateAsync(Menu menu);
        Task<bool> DeleteAsync(int id);
    }
}
