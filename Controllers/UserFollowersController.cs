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
    [Route("api/UserFollowers")]
    public class UserFollowersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserFollowersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserFollowers
        [HttpGet]
        public IEnumerable<UserFollow> GetUserFollower()
        {
            return _context.UserFollow;
        }

        [HttpGet("{id}")]
        public List<Post> GetFollowedPost(string id)

        {

            var follows = _context.UserFollow.Where(f => f.FollowingUserId == id).ToList();
            var posts = new List<Post>();
            foreach (var follow in follows)
            {
                var post = _context.Post.Where(a => a.ApplicationUserId == follow.FollowedUserId).FirstOrDefault();
                posts.Add(post);
            }




            return posts;
        }

        // GET: api/UserFollowers/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUserFollower([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var userFollower = await _context.UserFollower.SingleOrDefaultAsync(m => m.Id == id);

        //    if (userFollower == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(userFollower);
        //}

        // PUT: api/UserFollowers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserFollower([FromRoute] int id, [FromBody] UserFollow userFollow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userFollow.Id)
            {
                return BadRequest();
            }

            _context.Entry(userFollow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserFollowerExists(id))
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

        // POST: api/UserFollowers
        [HttpPost]
        public async Task<IActionResult> PostUserFollower([FromBody] UserFollow userFollow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserFollow.Add(userFollow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserFollower", new { id = userFollow.Id }, userFollow);
        }

        // DELETE: api/UserFollowers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserFollower([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userFollower = await _context.UserFollow.SingleOrDefaultAsync(m => m.Id == id);
            if (userFollower == null)
            {
                return NotFound();
            }

            _context.UserFollow.Remove(userFollower);
            await _context.SaveChangesAsync();

            return Ok(userFollower);
        }

        private bool UserFollowerExists(int id)
        {
            return _context.UserFollow.Any(e => e.Id == id);
        }
    }
}