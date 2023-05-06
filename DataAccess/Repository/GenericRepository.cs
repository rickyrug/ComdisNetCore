﻿using System;
using System.Linq.Expressions;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity:class
    {

		internal ComdisContext _context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(ComdisContext context)
		{
			this._context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter ,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy ,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual IEnumerable<TEntity> Get()
        {

            IQueryable<TEntity> query = dbSet;

            query.Take(50);

            return query.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            
            return  dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual bool Exist(object id)
        {
            var item = this.GetByID(id);
            if (item != null)
                return true;
            else
                return false;
        }
    }
}

