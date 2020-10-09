using System;
using System.Collections.Generic;
using System.Text;

namespace TvMaze.Data.Interfaces
{
    public interface ITvMazeRepository
    {
        void AddPersons(IEnumerable<Castmember> Castmembers);
        IEnumerable<int> GetCastmemberIdList();
        IEnumerable<CastShowLinkage> GetCastShowLinkage();
        void AddShowPersons(IEnumerable<CastShowLinkage> CastShowLinkages);
        void AddShows(IEnumerable<TVShow> Shows);
        IEnumerable<int> GetTVShowIdList();
        IEnumerable<TVShow> GetShows(int pagenumber);
        bool Save();
    }
}
