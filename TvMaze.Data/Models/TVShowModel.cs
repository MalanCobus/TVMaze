using System.Collections.Generic;

namespace TvMaze.Data
{
    public class TVShowModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Castmember> Castmembers { get; set; }
    }
}
