namespace Hooxit.Data.Contracts
{
    public interface IUpdateRepository<T>
    {
        void Update(T entity);
    }
}
