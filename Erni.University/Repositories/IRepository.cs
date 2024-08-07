namespace Erni.University.Repositories;

public interface IRepository<out T>
{
    IEnumerable<T> GetAll();
}