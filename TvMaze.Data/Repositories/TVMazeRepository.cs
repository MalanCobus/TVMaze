using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvMaze.Data.Interfaces;

namespace TvMaze.Data.Repositories
{
    public class TVMazeRepository : ITVMazeRepository
    {
        private readonly TvMazeContext _context;
        public TVMazeRepository(TvMazeContext context)
        {
            _context = context;
        }

        public void AddCastmembers(IEnumerable<Castmember> castmembers)
        {
            _context.Castmember.AddRange(castmembers);
        }

        public async Task<List<int>> GetCastmemberIdListAsync()
        {
            return await _context.Castmember.Select(p => p.Id).ToListAsync();
        }

        public void AddCastShowLinkage(IEnumerable<CastShowLinkage> CastShowLinkages)
        {
            _context.CastShowLinkage.AddRange(CastShowLinkages);
        }

        public async Task<List<CastShowLinkage>> GetCastShowLinkageAsync()
        {
            return await _context.CastShowLinkage.ToListAsync();
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
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
