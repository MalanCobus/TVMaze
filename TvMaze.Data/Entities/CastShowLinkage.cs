using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvMaze.Data
{
    public class CastShowLinkage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TVShowId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CastmemberId { get; set; }
        public TVShow TVShow { get; set; }
        public Castmember Castmember { get; set; }
    }
}
