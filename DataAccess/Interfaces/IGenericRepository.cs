using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
        IQueryable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter ,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "");
        IQueryable<TEntity> Get();
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        bool Exist(object id);
    }
}

