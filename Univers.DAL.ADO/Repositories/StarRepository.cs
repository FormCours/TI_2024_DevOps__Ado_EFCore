using Dapper;
using System.Data.Common;
using System.Dynamic;
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
            string sql = "INSERT INTO [Star]([Name], [IsDeath], [GalaxyId])" +
                " OUTPUT [inserted].*" +
                " VALUES (@Name, @IsDeath, @GalaxyId);";

            return _Connection.QuerySingle<Star>(sql, model);
        }

        public IEnumerable<Star> GetAll()
        {
            string sql = "SELECT * FROM [Star]";

            return _Connection.Query<Star>(sql);
        }

        public Star? GetById(int id)
        {
            string sql = "SELECT [S].*, [P].*, [G].*"
                    + " FROM [Star][S]"
                    + "  LEFT JOIN [Rel__Star_Planet] [SP] ON [S].[Id] = [SP].[StarId]"
                    + "  LEFT JOIN [Planet] [P] ON [P].[Id] = [SP].[PlanetId]"
                    + "  LEFT JOIN [Galaxy] [G] ON [G].[Id] = [S].[GalaxyId]"
                    + " WHERE [S].[Id] = @Id";

            // Parametre generique pour les jointures
            // - Le type des tables joints
            // - Le type du resultat
            // Via un délégué lui définir comment lier les données

            var result = _Connection.Query<Star, Planet, Galaxy, Star>(sql, (star, planet, galaxy) =>
            {
                star.Planets = [planet];
                star.Galaxy = galaxy;
                return star;

            }, new { Id = id });

            var star = result.GroupBy(elem => elem.Id).Select(grp =>
            {
                Star s = grp.First();
                s.Planets = grp.Select(g => g.Planets?.Single())?.ToList();
                return s;
            });

            return star.SingleOrDefault();
        }

        public bool AddPlanet(int id, int planetId)
        {
            string sql = "INSERT INTO [Rel__Star_Planet]([StarId], [PlanetId])" +
                " VALUES (@StarId, @PlanetId);";
            try
            {
                _Connection.Execute(sql, new
                {
                    StarId = id,
                    PlanetId = planetId
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddPlanets(int id, IEnumerable<int> planetIds)
        {
            List<int> currentPlanetIds = planetIds.ToList();
            List<string> sqlInputQuery = new List<string>();
            var sqlInputValue = new ExpandoObject() as IDictionary<string, object>;
            sqlInputValue.Add("StarId", id);

            for (int i = 0; i < currentPlanetIds.Count; i++)
            {
                sqlInputQuery.Add($"(@StarId, @PlanetId{i})");
                sqlInputValue.Add("PlanetId" + i, currentPlanetIds[i]);
            }

            string sql = "INSERT INTO [Rel__Star_Planet]([StarId], [PlanetId])" +
                $" VALUES {string.Join(",", sqlInputQuery)};";

            try
            {
                _Connection.Execute(sql, sqlInputValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemovePlanet(int id, int planetId)
        {
            string sql = "DELETE FROM [Rel__Star_Planet]" +
                " WHERE StarId = @StarId AND PlanetId = @PlanetId";

            try
            {
                _Connection.Execute(sql, new
                {
                    StarId = id,
                    PlanetId = planetId
                });
                return true;
            }
            catch
            {
                return false;
            }
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

            if (nbRow > 1)
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
