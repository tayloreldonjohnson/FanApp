using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hello.Data;
using Hello.Data.Models;

namespace Hello.Controllers
{
    [Produces("application/json")]
    [Route("api/Likes")]
    public class LikesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LikesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Likes
        [HttpGet]
        public IEnumerable<Like> GetLike()
        {
            return _context.Like;
        }

        // GET: api/Likes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLike([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var like = await _context.Like.SingleOrDefaultAsync(m => m.LikeId == id);

            if (like == null)
            {
                return NotFound();
            }

            return Ok(like);
        }

        // PUT: api/Likes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLike([FromRoute] int id, [FromBody] Like like)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != like.LikeId)
            {
                return BadRequest();
            }

            _context.Entry(like).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Likes
        [HttpPost]
        public async Task<IActionResult> PostLike([FromBody] Like like)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Like.Add(like);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLike", new { id = like.LikeId }, like);
        }

        // DELETE: api/Likes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLike([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var like = await _context.Like.SingleOrDefaultAsync(m => m.LikeId == id);
            if (like == null)
            {
                return NotFound();
            }

            _context.Like.Remove(like);
            await _context.SaveChangesAsync();

            return Ok(like);
        }

        private bool LikeExists(int id)
        {
            return _context.Like.Any(e => e.LikeId == id);
        }
        // Likes with no duplicates
        //[HttpPost]
        //public async Task<IActionResult> LikesWithNoDuplicates([FromBody] Like like)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var duplicates = _context.Like.Where(uf => uf.Like == LikeId.UserId && uf.Like == LikeId.PostId).Count();

        //    if (duplicates > 0 || LikeId.UserId == LikeId.PostId)
        //    {

        //        var error = new
        //        {
        //            message = "You are either liked this post lready or attempting to like again",
        //            status = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError
        //        };
        //        //Context.Response.StatusCode = error.status;
        //        return new ObjectResult(error);


        //    }

        //    _context.UserLike.Add(LikeId);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUserLike", new { id = LikeId.Id }, likeId);
        //}

    }
}