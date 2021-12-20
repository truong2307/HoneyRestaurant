using Honey.Web.Models;
using Honey.Web.Models.Dto;
using Honey.Web.Services.IServices;
using System.Net.Http;
using System.Threading.Tasks;

namespace Honey.Web.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CategoryService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateCategoryAsync<T>(CategoryDto categoryRequest)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ProductAPIBase = "/api/categories",
                Data = categoryRequest,
                AccessToken = ""
            });
        }

        public async Task<T> DeleteCategoryAsync<T>(int categoryId)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase = "/api/categories"+categoryId,
                AccessToken = ""
            });
        }

        public async Task<T> GetAllCategoriessAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase = "/api/categories",
                AccessToken = ""
            });
        }

        public async Task<T> GetCategoryByIdAsync<T>(int categoryId)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase = "/api/categories" + categoryId,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateCategoryAsync<T>(CategoryDto categoryRequest)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Data = categoryRequest,
                Url = SD.ProductAPIBase = "/api/categories",
                AccessToken = ""
            });
        }
    }
}
