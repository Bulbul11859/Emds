using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using System.Linq.Expressions;
namespace Repository.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly EmployeeManagementDbContext _db;
    public BaseRepository(EmployeeManagementDbContext db)
    {
        _db = db;
    }
    public void Create(T entity)
    { 
        _db.Set<T>().Add(entity);
    }
    public void Update(T entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
    }
    public void Delete(T entity)
    {
        _db.Set<T>().Remove(entity);
    }
    public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
    {
        return _db.Set<T>().Where(x => x.IsDeleted != true).Where(predicate);
    }
    public T GetById(int id)
    {
        return _db.Set<T>().Find(id);
    }

    public IEnumerable<T> GetAllEnumerable()
    {
        return _db.Set<T>().AsEnumerable();
    }

    public IQueryable<T> GetAllQueryable()
    {
        return _db.Set<T>();
    }
    public bool Any()
    {
        return _db.Set<T>().Where(w => w.IsDeleted != true).Any();
    }

    public bool Any(Expression<Func<T, bool>> predicate)
    {
        return _db.Set<T>().Where(w => w.IsDeleted != true).Any(predicate);
    }
    public T FirstOrDefault(Expression<Func<T, bool>> predicate)
    {
        return _db.Set<T>().Where(w => w.IsDeleted != true).FirstOrDefault(predicate);
    }
    public T FirstOrDefault()
    {
        return _db.Set<T>().Where(w => w.IsDeleted != true).FirstOrDefault();
    }
}
