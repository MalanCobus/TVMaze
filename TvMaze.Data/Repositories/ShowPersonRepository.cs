using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMaze.Data.Interfaces;

namespace TvMaze.Data.Repositories
{
    public class ShowPersonRepository : ICastShowLinkageRepository
    {
        private readonly TvMazeContext _context;
        public ShowPersonRepository(TvMazeContext context)
        {
            _context = context;
        }

        public void AddCastShowLinkage(IEnumerable<CastShowLinkage> CastShowLinkages)
        {
            _context.CastShowLinkage.AddRange(CastShowLinkages);
        }

        public async Task<List<CastShowLinkage>> GetCastShowLinkageAsync()
        { 
            return await _context.CastShowLinkage.ToListAsync();
        }
        
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
