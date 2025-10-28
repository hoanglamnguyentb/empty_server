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
    public class ProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();

            return list.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
            }).ToList();
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var x = await _repo.GetByIdAsync(id);
            if (x == null) return null;

            return new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            };
        }

        public async Task CreateAsync(ProductDto dto)
        {
            var p = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };

            await _repo.AddAsync(p);
        }

        public async Task<bool> UpdateAsync(int id, ProductDto dto)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return false;

            p.Name = dto.Name;
            p.Price = dto.Price;

            await _repo.UpdateAsync(p);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return false;

            await _repo.DeleteAsync(p);
            return true;
        }
    }
}
