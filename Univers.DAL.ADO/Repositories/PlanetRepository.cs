using System.Data;
using System.Data.Common;
using System.Reflection;
using Univers.Common.Models;
using Univers.Common.Repositories;

namespace Univers.DAL.ADO.Repositories
{
    public class PlanetRepository : RepositoryBase, IPlanetRepository 
    {
        public PlanetRepository(DbConnection connection)
            : base(connection) { }

        protected Planet MapToPlanet(IDataRecord record)
        {
            return new Planet()
            {
                Id = (int)record["Id"],
                //Id = record.GetInt32(record.GetOrdinal("Id")),
                //Id = Convert.ToInt32(record["Id"]),
                Name = (string)record["Name"],
                Satelite = (int)record["Satelite"],
                Gravity = (int)record["Gravity"],
            };
        } 

        public Planet Create(Planet model)
        {
            using DbCommand cmd = _Connection.CreateCommand();

            cmd.CommandText = "INSERT INTO [Planet] ([Name], [Satelite], [Gravity])"
                            + " OUTPUT [inserted].*"
                            + " VALUES (@Name, @Satelite, @Gravity);";
            AddCommandParameter(cmd, "Name", model.Name);
            AddCommandParameter(cmd, "Satelite", model.Satelite);
            AddCommandParameter(cmd, "Gravity", model.Gravity);

            using DbDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                throw new Exception("Fail on create Planet");
            }

            return MapToPlanet(reader);
        }

        public IEnumerable<Planet> GetAll()
        {
            using DbCommand cmd = _Connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM [Planet]";

            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return MapToPlanet(reader);
            }
        }

        public Planet? GetById(int id)
        {
            using DbCommand cmd = _Connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM [Planet] WHERE Id = @Id";
            AddCommandParameter(cmd, "Id", id);

            using DbDataReader reader = cmd.ExecuteReader();

            Planet? data = null;
            if (reader.Read())
            {
                data = MapToPlanet(reader);
            }
            if (reader.Read())
            {
                throw new Exception("Two planet with same id");
            }

            return data;
        }

        public bool Update(int id, Planet data)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
