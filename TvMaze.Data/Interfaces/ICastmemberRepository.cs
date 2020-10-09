using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TvMaze.Data.Interfaces
{
    public interface ICastmemberRepository
    {
        void AddCastmembers(IEnumerable<Castmember> Castmembers);
        Task<List<int>> GetCastmemberIdListAsync();
        bool Save();
    }
}
