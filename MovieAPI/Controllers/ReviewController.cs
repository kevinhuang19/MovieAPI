using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Services;
using System.Collections.Generic;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/v1/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly MongoDBService _reviewService;

        public ReviewController(MongoDBService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public IActionResult CreateReview([FromBody] Dictionary<string, string> payload)
        {
            if (!payload.ContainsKey("reviewBody") || !payload.ContainsKey("imdbId"))
            {
                return BadRequest("Review body or IMDb ID not provided");
            }

            string reviewBody = payload["reviewBody"];
            string imdbId = payload["imdbId"];

            var review = _reviewService.createReview(reviewBody, imdbId);

            if (review == null)
            {
                return StatusCode(500, "Failed to create review");
            }

            return CreatedAtAction(nameof(_reviewService.createReview), new { id = review.Id }, review);
        }
    }
}
