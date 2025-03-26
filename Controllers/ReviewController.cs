using AutoMapper;
using E_handelWEBapplication.DTOs;
using E_handelWEBapplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace E_handelWEBapplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly EHandelWebappContext _context;
        private readonly IMapper _mapper;

        public ReviewController(EHandelWebappContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Review
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviews()
        {
            var reviews = await _context.Reviews.Include(r => r.Product).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ReviewDto>>(reviews));
        }

        // GET: api/Review/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReviewById(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();

            return Ok(_mapper.Map<ReviewDto>(review));
        }

        // POST: api/Review
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> CreateReview([FromBody] ReviewDto reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, _mapper.Map<ReviewDto>(review));
        }

        // PUT: api/Review/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, ReviewDto reviewDto)
        {
            if (id != reviewDto.Id) return BadRequest("Id mismatch");

            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();

            _mapper.Map(reviewDto, review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Review/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
