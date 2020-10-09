using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TvMaze.Data.Interfaces
{
    public interface ITVShowRepository
    {
        void AddShow(TVShow Show);
        void AddShows(IEnumerable<TVShow> Shows);
        Task<List<int>> GetShowIdListAsync();
        IEnumerable<TVShow> GetShows(int page, int size);
        IEnumerable<TVShowModel> GetShowModels(int page, int size);
        TVShowModel GetShowModel(int id);
        bool Save();
    }
}
