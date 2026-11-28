using System.Collections.Generic;

namespace HeThongQuanLyDuAn.Interfaces;

public interface IRepository<T> where T : class, IEntity
{
    List<T> GetAll();
    T? GetById(string id);
    void Add(T entity);
    void Update(T entity);
    bool Delete(string id);
}