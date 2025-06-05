using System.Data;
using System.Data.Common;
using Univers.Common.Models;
using Univers.Common.Repositories;

namespace Univers.DAL.ADO.Repositories
{
    public class GalaxyRepository : RepositoryBase, IGalaxyRepository
    {
        public GalaxyRepository(DbConnection connection)
            : base(connection) { }


        public Galaxy Create(Galaxy model)
        {
            using DbCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = "INSERT INTO [Galaxy]([Name], [Description]) OUTPUT [inserted].* VALUES(@Name, @Description)";
            AddCommandParameter(cmd, "Name", model.Name);
            AddCommandParameter(cmd, "Description", model.Description);

            using DbDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                throw new Exception("Fail on create Galaxy");
            }

            return MapToGalaxy(reader);
        }

        public IEnumerable<Galaxy> GetAll()
        {
            using DbCommand cmd = _Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM [Galaxy]";

            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return MapToGalaxy(reader);
            }
        }

        public Galaxy? GetById(int id)
        {
            using DbCommand cmd = _Connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM [Galaxy] WHERE [Id] = @id";
            AddCommandParameter(cmd, "id", id);

            using DbDataReader reader = cmd.ExecuteReader();

            Galaxy? galaxy = null;
            if (reader.Read())
            {
                galaxy = MapToGalaxy(reader);
            }
            if (reader.Read())
            {
                throw new Exception("Two Galaxy with same id");
            }
            return galaxy;
        }

        private Galaxy MapToGalaxy(IDataRecord record)
        {
            return new Galaxy()
            {
                Id = (int)record["Id"],
                Name = (string)record["Name"],
                Description = record["Description"] is DBNull ? null : (string)record["Description"],
                Stars = null
            };
        }

        public bool Update(int id, Galaxy data)
        {
            using DbTransaction transaction = _Connection.BeginTransaction();
            using DbCommand cmd = _Connection.CreateCommand();
            cmd.Transaction = transaction;
            
            cmd.CommandText = "Update Galaxy " +
                "SET Name=@Name, Description=@Description " +
                "WHERE Id = @Id";
            AddCommandParameter(cmd, "Name", data.Name);
            AddCommandParameter(cmd, "Description", data.Description);
            AddCommandParameter(cmd, "Id", id);

            int nbRow = cmd.ExecuteNonQuery();
            if(nbRow > 1)
            {
                transaction.Rollback();
                throw new Exception("Two Galaxy with same id");
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
