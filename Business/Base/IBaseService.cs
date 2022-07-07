using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Base
{
    public interface IBaseService<TEntity>
        where TEntity : class, IEntity, new()
    {
        public Task<TEntity> Get(int? id);
        public Task<List<TEntity>> GetAll();
        public Task Create(TEntity entity);
        public Task Update(int id, TEntity entity);
        public Task Delete(int? id);
    }
}
