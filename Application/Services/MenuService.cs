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
    public class MenuService
    {
        private readonly IMenuRepository _repo;

        public MenuService(IMenuRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Menu>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<List<Menu>> GetTreeAsync()
            => await _repo.GetTreeAsync();

        public async Task<Menu?> GetByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<Menu> CreateAsync(Menu menu)
        {
            await _repo.AddAsync(menu);
            return menu;
        }

        public async Task<Menu> UpdateAsync(Menu menu)
        {
            await _repo.UpdateAsync(menu);
            return menu;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var menu = await _repo.GetByIdAsync(id);
            if (menu == null) return false;

            await _repo.DeleteAsync(menu);
            return true;
        }
    }

}
