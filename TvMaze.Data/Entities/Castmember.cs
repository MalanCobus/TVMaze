using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvMaze.Data
{
    public class Castmember
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        //public IEnumerable<CastShowLinkage> CastShowLinkage { get; set; }
    }
}
