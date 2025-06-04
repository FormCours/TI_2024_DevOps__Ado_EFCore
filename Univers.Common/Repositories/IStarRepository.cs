using Univers.Common.Models;

namespace Univers.Common.Repositories
{
    public interface IStarRepository : ICrudRepository<int, Star>
    {
        bool AddPlanet(int id, Planet planet);
        bool AddPlanets(int id, IEnumerable<Planet> planets);

        bool RemovePlanet(int id, int planetId);
    }
}
