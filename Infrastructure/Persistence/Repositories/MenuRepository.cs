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

        public async Task<List<Menu>> GetTreeAsync()
        {
            var menus = await _dbSet.OrderBy(m => m.SortOrder).ToListAsync();

            var lookup = menus.ToLookup(m => m.ParentId);

            foreach (var menu in menus)
            {
                menu.Children = lookup[menu.Id].ToList();
            }

            return lookup[null].ToList();
        }
    }
}
