using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMaze.Data.Interfaces;

namespace TvMaze.Data.Repositories
{
    public class PersonRepository : ICastmemberRepository
    {
        private readonly TvMazeContext _context;
        public PersonRepository(TvMazeContext context)
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

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
