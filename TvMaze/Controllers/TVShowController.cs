using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TvMaze.Data;
using TvMaze.Data.Interfaces;

namespace TvMaze.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVShowController : ControllerBase
    {
        private readonly ITVShowRepository _showRepository;

        public TVShowController(ITVShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        [HttpGet("{pageNumber}", Name = "Get")]
        [ProducesResponseType(typeof(IEnumerable<TVShowModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int pageNumber)
        {
            return Ok(_showRepository.GetShowModels(pageNumber, 50));
        }
    }
}