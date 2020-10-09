using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvMaze.Data
{
    public class TVShow
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CastShowLinkage> CastShowLinkage { get; set; }
    }
}
