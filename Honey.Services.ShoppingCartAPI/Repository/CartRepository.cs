using AutoMapper;
using Honey.Services.ShoppingCartAPI.DbContexts;
using Honey.Services.ShoppingCartAPI.Model;
using Honey.Services.ShoppingCartAPI.Model.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Honey.Services.ShoppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CartRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> ClearCart(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartRequest)
        {
            Cart cartToDb = _mapper.Map<Cart>(cartRequest);

            var productInDb = await _db.Products
                .FirstOrDefaultAsync(c =>
                c.ProductId == cartToDb.CartDetails.FirstOrDefault().ProductId);

            //Check if product exists in database, if not create it
            if (productInDb == null)
            {
                _db.Products.Add(cartToDb.CartDetails.FirstOrDefault().Product);
                await _db.SaveChangesAsync();
            }

            var cartHeaderInDb = await _db.CartHeaders.AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == cartToDb.CartHeader.UserId);

            //check if cart header is null
            if (cartHeaderInDb == null)
            {
                //Create cart header and details
                _db.CartHeaders.Add(cartToDb.CartHeader);
                await _db.SaveChangesAsync();

                cartToDb.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderInDb.CartHeaderId;
                cartToDb.CartDetails.FirstOrDefault().Product = null;

                _db.CartDetails.Add(cartToDb.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                //if cart header not null
                //check if detail has same product
                var cartDetailInDb = await _db.CartDetails.AsNoTracking()
                    .FirstOrDefaultAsync(c =>
                    c.ProductId == cartToDb.CartDetails.FirstOrDefault().ProductId
                    && c.CartHeaderId == cartHeaderInDb.CartHeaderId);

                if (cartDetailInDb == null)
                {
                    cartToDb.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderInDb.CartHeaderId;
                    cartToDb.CartDetails.FirstOrDefault().Product = null;

                    _db.CartDetails.Add(cartToDb.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
                else
                {
                    //update the count / cart details
                    cartToDb.CartDetails.FirstOrDefault().Product = null;
                    cartToDb.CartDetails.FirstOrDefault().Count += cartDetailInDb.Count;
                    _db.CartDetails.Update(cartToDb.CartDetails.FirstOrDefault());

                    await _db.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartDto>(cartToDb);
        }

        public async Task<CartDto> GetCartById(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            throw new System.NotImplementedException();
        }
    }
}