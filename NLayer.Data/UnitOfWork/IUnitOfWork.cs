namespace NLayer.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        void CommitAsync();
    }
}
