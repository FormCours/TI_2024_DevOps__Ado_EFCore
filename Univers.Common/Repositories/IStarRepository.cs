using Univers.Common.Models;

namespace Univers.Common.Repositories
{
    public interface IStarRepository : ICrudRepository<int, Star>
    {
        bool AddPlanet(int id, int planetId);
        bool AddPlanets(int id, IEnumerable<int> planetIds);

        bool RemovePlanet(int id, int planetId);
    }
}
