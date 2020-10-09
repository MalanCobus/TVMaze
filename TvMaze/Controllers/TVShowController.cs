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
        private readonly ITVMazeRepository _tvMazeRepository;

        public TVShowController(ITVMazeRepository tvMazeRepository)
        {
            _tvMazeRepository = tvMazeRepository;
        }

        [HttpGet("{pageNumber}", Name = "Get")]
        [ProducesResponseType(typeof(IEnumerable<TVShowModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int pageNumber, int pageSize = 50)
        {
            return Ok(_tvMazeRepository.GetShowModels(pageNumber, pageSize));
        }
    }
}