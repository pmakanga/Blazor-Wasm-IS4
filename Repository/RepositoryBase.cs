﻿using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext repositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
              repositoryContext.Set<T>()
                .AsNoTracking() :
              repositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges) =>
            !trackChanges ?
              repositoryContext.Set<T>()
                .Where(expression)
                .AsNoTracking() :
              repositoryContext.Set<T>()
                .Where(expression);

        public void Create(T entity) => repositoryContext.Set<T>().Add(entity);

        public void Update(T entity) => repositoryContext.Set<T>().Update(entity);

        public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);
        //public void Delete(T entity)
        //{
        //    repositoryContext.Set<T>().Remove(entity);
        //}
    }
}
