using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepositoryService<TEntity> where TEntity : class
    {
       Task<bool> Add(TEntity model);
       Task<bool> Delete(Guid id);
       Task<TEntity> GetById(Guid id);
       Task<IEnumerable<TEntity>> GetAll();
       Task<bool> Update(TEntity model);
    }
}
