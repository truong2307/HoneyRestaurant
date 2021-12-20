using Honey.Services.ProductAPI.Models.Dto;
using Honey.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Honey.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        protected ResponseDto _response;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> GetProducts()
        {
            try
            {
                IEnumerable<ProductDto> listProductInDb = await _unitOfWork.Product.GetProducts();
                _response.Result = listProductInDb;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpGet("{productId}")]
        public async Task<object> GetProduct(int productId)
        {
            try
            {
                ProductDto productInDb = await _unitOfWork.Product.GetProductById(productId);
                _response.Result = productInDb;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPost]
        public async Task<object> CreateProduct(ProductDto productRequest)
        {
            try
            {
                ProductDto productInDb = await _unitOfWork.Product.CreateProduct(productRequest);
                _response.Result = productInDb;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPut]
        public async Task<object> UpdateProduct(ProductDto productRequest)
        {
            try
            {
                ProductDto productInDb = await _unitOfWork.Product.UpdateProduct(productRequest);
                _response.Result = productInDb;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpDelete("{productId}")]
        public async Task<object> DeleteProduct(int productId)
        {
            try
            {
                bool productIsDeleted = await _unitOfWork.Product.DeleteProduct(productId);
                _response.Result = productIsDeleted;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }
    }
}
