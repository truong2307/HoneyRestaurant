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

        public async Task<T> CreateCategoryAsync<T>(CategoryDto categoryRequest, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ProductAPIBase + "api/categories",
                Data = categoryRequest,
                AccessToken = token
            });
        }

        public async Task<T> DeleteCategoryAsync<T>(int categoryId, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "api/categories/"+categoryId,
                AccessToken = token
            });
        }

        public async Task<T> GetAllCategoriessAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "api/categories",
                AccessToken = token
            });
        }

        public async Task<T> GetCategoryByIdAsync<T>(int categoryId, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "api/categories/" + categoryId,
                AccessToken = token
            });
        }

        public async Task<T> UpdateCategoryAsync<T>(CategoryDto categoryRequest, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = categoryRequest,
                Url = SD.ProductAPIBase + "api/categories",
                AccessToken = token
            });
        }
    }
}
