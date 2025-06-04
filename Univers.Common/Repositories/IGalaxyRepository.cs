using Univers.Common.Models;

namespace Univers.Common.Repositories
{
    public interface IGalaxyRepository : ICrudRepository<int, Galaxy>
    {
        bool AddStar(int id, Star star);
    }
}
