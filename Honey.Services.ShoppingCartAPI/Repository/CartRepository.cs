using AutoMapper;
using Honey.Services.ShoppingCartAPI.DbContexts;
using Honey.Services.ShoppingCartAPI.Model;
using Honey.Services.ShoppingCartAPI.Model.Dto;
using Microsoft.EntityFrameworkCore;
using System;
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
            var cartHeaderFromDb = await _db.CartHeaders
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cartHeaderFromDb != null)
            {
                var cartDetailsToRemove = (from cartDetail in _db.CartDetails
                                          where cartDetail.CartHeaderId == cartHeaderFromDb.CartHeaderId
                                          select cartDetail).ToList();

                _db.CartDetails.RemoveRange(cartDetailsToRemove);
                _db.CartHeaders.Remove(cartHeaderFromDb);
                await _db.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartRequest)
        {
            Cart cartToDb = _mapper.Map<Cart>(cartRequest);

            var productInDb = await _db.Products
                .FirstOrDefaultAsync(c =>
                c.ProductId == cartToDb.CartDetails.FirstOrDefault().ProductId);

            var categoryInDb = await _db.Categories
                .FirstOrDefaultAsync(c =>
                c.CategoryId == cartToDb.CartDetails.FirstOrDefault().Product.CategoryId);

            //Check if category exists in database, if not create it
            if (categoryInDb == null)
            {
                _db.Categories.Add(cartToDb.CartDetails.FirstOrDefault().Product.Category);
                await _db.SaveChangesAsync();
            }

            //Check if product exists in database, if not create it
            if (productInDb == null)
            {
                cartToDb.CartDetails.FirstOrDefault().Product.Category = null;
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

                var cartHeader = await _db.CartHeaders.AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == cartToDb.CartHeader.UserId);

                cartToDb.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.CartHeaderId;
                cartToDb.CartDetails.FirstOrDefault().Product = null;

                _db.CartDetails.Add(cartToDb.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                //if cart header not null
                //check if detail has same product
                var cartDetailInDb = await _db.CartDetails
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
                    cartDetailInDb.Count += cartToDb.CartDetails.FirstOrDefault().Count;

                    //_db.CartDetails.Update(cartToDb.CartDetails.FirstOrDefault());

                    await _db.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartDto>(cartToDb);
        }

        public async Task<CartDto> GetCartById(string userId)
        {
            Cart cartResponse = new Cart()
            {
                CartHeader = await _db.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId)
            };

            if (cartResponse.CartHeader != null)
            {
                cartResponse.CartDetails = (from cartDetail in _db.CartDetails
                                            where cartDetail.CartHeaderId == cartResponse.CartHeader.CartHeaderId
                                            select cartDetail).Include(c => c.Product).ToList();
            }
            
            return _mapper.Map<CartDto>(cartResponse);
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetails cartDetailInDb = await _db.CartDetails
                .FirstOrDefaultAsync(c => c.CartDetailsId == cartDetailsId);

                int totalCountOfCartItem = (from cartDetail in _db.CartDetails
                                            where cartDetail.CartHeaderId == cartDetailInDb.CartHeaderId
                                            select cartDetail).Count();
                _db.CartDetails.Remove(cartDetailInDb);

                //if total item == 1 then delete cart header
                if (totalCountOfCartItem == 1)
                {
                    var cartHeaderToRemove = await _db.CartHeaders
                        .FirstOrDefaultAsync(c => c.CartHeaderId == cartDetailInDb.CartHeaderId);

                    _db.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> MinusPlusCart(int cartDetailId, int cartHeaderId, bool isPlus)
        {
            var cartDetailInDb = _db.CartDetails
                .FirstOrDefault(c => c.CartDetailsId == cartDetailId && c.CartHeaderId == cartHeaderId);

            if (cartDetailInDb != null)
            {
                switch (true)
                {
                    case true when isPlus:
                        cartDetailInDb.Count += 1;
                        break;
                    default:
                        cartDetailInDb.Count -= 1;
                        if (cartDetailInDb.Count == 0) await RemoveFromCart(cartDetailId);
                        break;
                }
                
                await _db.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}