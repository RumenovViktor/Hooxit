namespace Hooxit.Data.Contracts
{
    public interface IDeleteRepository<T>
    {
        void Delete(T entity);
    }
}
