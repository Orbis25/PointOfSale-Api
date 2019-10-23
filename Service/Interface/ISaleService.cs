using Model.Models;
using Model.ViewModels.Sale;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ISaleService : IRepositoryService<Sale>
    {
        Task<bool> AddSV(SaleVM model);
        Task<IEnumerable<Product>> GetsProductBySaleId(Guid id);
    }
}
