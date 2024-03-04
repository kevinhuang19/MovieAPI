using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Services;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/v1/movies")]

    public class MovieController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;

        public MovieController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<Movies>> Get() {
            return await _mongoDBService.getAllMovies();
        }

        [HttpGet("{imdbId}")]
        public async Task<ActionResult<Movies>> GetSingleMovieByImdbId(string imdbId)
        {
            var movie = await _mongoDBService.getSingleMovie(imdbId);

            if(movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

    }
}
