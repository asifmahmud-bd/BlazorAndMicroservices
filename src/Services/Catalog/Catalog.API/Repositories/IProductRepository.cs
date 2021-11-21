using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsc();
        Task<Product> GetProductByIdAsc(string id);
        Task<IEnumerable<Product>> GetProductByNameAsc(string name);
        Task<IEnumerable<Product>> GetProductByCategoryAsc(string category);

        Task CreateProduct(Product product);
        Task<bool> UpdateProductAsc(Product product);
        Task<bool> DeleteProductAsc(string id);
    }
}
