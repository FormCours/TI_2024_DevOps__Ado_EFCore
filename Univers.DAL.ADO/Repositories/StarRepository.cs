using Dapper;
using System.Data.Common;
using Univers.Common.Models;
using Univers.Common.Repositories;

namespace Univers.DAL.ADO.Repositories
{
    public class StarRepository : RepositoryBase, IStarRepository
    {
        public StarRepository(DbConnection connection)
            : base(connection) { }

        public Star Create(Star model)
        {
            string sql = "INSERT INTO [Star]([Name], [IsDeath])" +
                " OUTPUT [inserted].*" +
                " VALUES (@Name, @IsDeath);";

            return _Connection.QuerySingle<Star>(sql, model);
        }

        public IEnumerable<Star> GetAll()
        {
            string sql = "SELECT * FROM [Star]";

            return _Connection.Query<Star>(sql);
        }

        public Star? GetById(int id)
        {
            string sql = "SELECT * FROM [Star] WHERE [Id] = @Id";

            return _Connection.QuerySingleOrDefault<Star>(sql, new { Id = id });
        }

        public bool AddPlanet(int id, int planetId)
        {
            throw new NotImplementedException();
        }

        public bool AddPlanets(int id, IEnumerable<int> planetIds)
        {
            throw new NotImplementedException();
        }

        public bool RemovePlanet(int id, int planetId)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, Star data)
        {
            using DbTransaction transaction = _Connection.BeginTransaction();

            string sql = "UPDATE [Star]" +
                " SET Name = @Name," +
                "     IsDeath = @IsDeath" +
                " WHERE Id = @Id";

            int nbRow = _Connection.Execute(sql, new
            {
                Id = id,
                Name = data.Name,
                IsDeath = data.IsDeath,
            }, transaction);

            if(nbRow > 1)
            {
                transaction.Rollback();
                throw new Exception("Two Star with same id");
            }

            transaction.Commit();
            return nbRow == 1;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
