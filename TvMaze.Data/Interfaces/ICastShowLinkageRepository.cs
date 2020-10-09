using System.Collections.Generic;
using System.Threading.Tasks;

namespace TvMaze.Data.Interfaces
{
    public interface ICastShowLinkageRepository
    {
        Task<List<CastShowLinkage>> GetCastShowLinkageAsync();
        void AddCastShowLinkage(IEnumerable<CastShowLinkage> CastShowLinkage);
        bool Save();
    }
}
