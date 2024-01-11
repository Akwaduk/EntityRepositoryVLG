using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public abstract class BaseService<T> where T : class
{
    protected readonly DbContext _dbContext;

    public BaseService(DbContext dbContext,
        bool EnableLogging = false,
        bool EnableVersioning = false)
    {
        _dbContext = dbContext;
    }

    public virtual T Create(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public virtual IEnumerable<T> Read(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? includeProperties = null,
        int? skip = null)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query.ToList();
    }
    

    public virtual T Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public virtual void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        _dbContext.SaveChanges();
    }
}
