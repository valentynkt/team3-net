using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);

        Task Add(TEntity entity);

        Task Update(TEntity entity);
        Task Delete(TEntity entity);

        Task DeleteById(int id);
    }
}
