using AutoMapper;
using Honey.Services.ProductAPI.DbContexts;
using Honey.Services.ProductAPI.Models;
using Honey.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Honey.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProduct(ProductDto productRequest)
        {
            Product productToDb = _mapper.Map<Product>(productRequest);
            _db.Products.Add(productToDb);
            await _db.SaveChangesAsync();

            return _mapper.Map<ProductDto>(productToDb);
        }

        public async Task<ProductDto> UpdateProduct(ProductDto productRequest)
        {
            Product productInDb = await _db.Products.Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.ProductId == productRequest.ProductId);

            productInDb.Name = productRequest.Name;
            productInDb.Price = productRequest.Price;
            productInDb.Description = productRequest.Description;
            productInDb.CategoryId = productRequest.CategoryId;
            productInDb.ImageUrl = productRequest.ImageUrl;

            return _mapper.Map<ProductDto>(productInDb);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Product productInDb = await _db.Products.FirstOrDefaultAsync(c => c.ProductId == productId);
                if (productInDb == null)
                {
                    return false;
                }
                _db.Products.Remove(productInDb);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            Product productInDb = await _db.Products
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.ProductId == productId);

            return _mapper.Map<ProductDto>(productInDb);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> listProductInDb = await _db.Products
                .Include(c => c.Category)
                .ToListAsync();

            return _mapper.Map<List<ProductDto>>(listProductInDb);
        }
    }
}
