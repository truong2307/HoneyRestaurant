using System;

namespace Honey.Services.ProductAPI.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
    }
}
