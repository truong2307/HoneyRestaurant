using AutoMapper;
using Honey.Services.ProductAPI.DbContexts;

namespace Honey.Services.ProductAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper; 

        public UnitOfWork(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            Product = new ProductRepository(_db, _mapper);
            Category = new CategoryRepository(_db, _mapper);
        }

        public IProductRepository Product { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
