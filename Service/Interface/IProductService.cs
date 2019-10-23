using Model.Models;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IProductService : IRepositoryService<Product>
    {
        Task<bool> UpdateAvatar(Product model);
        Task<IEnumerable<Product>> ProductsSpent();
    }
}
