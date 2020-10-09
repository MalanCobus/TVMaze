using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using TvMaze.Data.Interfaces;

namespace TvMaze.Data.Repositories
{
    public class ShowRepository : ITVShowRepository
    {
        private TvMazeContext _context;
        public ShowRepository(TvMazeContext context)
        {
            _context = context;
        }

        public void AddShow(TVShow Show)
        {
            _context.TVShows.Add(Show);
        }

        public void AddShows(IEnumerable<TVShow> Shows)
        {
            _context.TVShows.AddRange(Shows);
        }

        public TVShowModel GetShowModel(int id)
        {
            return _context.TVShows
                .Select(s =>
               new TVShowModel()
               {
                   Id = s.Id,
                   Castmembers = s.CastShowLinkage.Select(c => c.Castmember).OrderByDescending(p => p.Birthday)
               })
                .FirstOrDefault(s => s.Id == id);
        }

        public Task<List<int>> GetShowIdListAsync()
        {
            return _context.TVShows.Select(s => s.Id).ToListAsync();
        }

        public IEnumerable<TVShow> GetShows(int page, int size)
        {
            return _context.TVShows.Include(s => s.CastShowLinkage)
                .ThenInclude(sp => sp.Castmember)
                .Skip(page * size)
                .Take(size)
                .ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<TVShowModel> GetShowModels(int page, int size)
        {
            return _context.TVShows
                .Skip(page * size)
                .Take(size)
                .Select(s =>
               new TVShowModel()
               {
                   Id = s.Id,
                   Name = s.Name,
                   Castmembers = s.CastShowLinkage.Select(c => c.Castmember).OrderByDescending(p => p.Birthday)
               });
        }
    }
}
