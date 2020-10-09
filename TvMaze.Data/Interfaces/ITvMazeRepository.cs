using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TvMaze.Data.Interfaces
{
    public interface ITVMazeRepository
    {
        void AddCastmembers(IEnumerable<Castmember> castmembers);
        Task<List<int>> GetCastmemberIdListAsync();
        void AddCastShowLinkage(IEnumerable<CastShowLinkage> CastShowLinkages);
        Task<List<CastShowLinkage>> GetCastShowLinkageAsync();
        void AddShow(TVShow Show);
        void AddShows(IEnumerable<TVShow> Shows);
        TVShowModel GetShowModel(int id);
        Task<List<int>> GetShowIdListAsync();
        IEnumerable<TVShow> GetShows(int page, int size);
        IEnumerable<TVShowModel> GetShowModels(int page, int size);
        bool Save();
    }
}
