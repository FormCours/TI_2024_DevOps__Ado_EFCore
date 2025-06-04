namespace Univers.Common.Repositories
{
    public interface ICrudRepository<TId, TModel>
    {
        // Create
        TModel Create(TModel model);

        // Read
        TModel? GetById(TId id);
        IEnumerable<TModel> GetAll();

        // Update
        bool Update(TId id, TModel data);

        // Delete
        bool Delete(TId id);
    }
}
