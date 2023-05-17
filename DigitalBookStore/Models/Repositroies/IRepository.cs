namespace DigitalBookStore.Models.Repositroies
{
    public interface IRepository <TEntity>{

        IList <TEntity> List();

        TEntity Find(int id);

        void Add(TEntity entity);

        void Update(int id, TEntity entity);

        void Delete(int id);

        IList<TEntity> Search(string term);

    }
}
