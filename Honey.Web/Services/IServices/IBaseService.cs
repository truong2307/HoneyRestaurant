using Honey.Web.Models;
using Honey.Web.Models.Dto;
using System;
using System.Threading.Tasks;

namespace Honey.Web.Services.IServices
{
    public interface IBaseService :IDisposable
    {
        ResponseDto responseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest); 
    }
}
