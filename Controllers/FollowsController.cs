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
	[Route("api/Follows")]
	public class FollowsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public FollowsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: api/Follows
		[HttpGet]
		public IEnumerable<Follow> GetFollow()
		{
			return _context.Follow;
		}

		// GET: api/Follows/5
		//[HttpGet("{id}")]
		//public async Task<IActionResult> GetFollow([FromRoute] int id)
		//{
		//    if (!ModelState.IsValid)
		//    {
		//        return BadRequest(ModelState);
		//    }

		//    var follow = await _context.Follow.SingleOrDefaultAsync(m => m.Id == id);

		//    if (follow == null)
		//    {
		//        return NotFound();
		//    }

		//    return Ok(follow);
		//}



		[HttpGet("{id}")]
		public List<ApplicationArtist> GetFollowedArtist(string id)

		{

			var follows = _context.Follow.Where(f => f.FollowerId == id).ToList();
			var artists = new List<ApplicationArtist>();
			foreach (var follow in follows)
			{
				var artist = _context.ApplicationArtist.Where(a => a.Id == follow.FollowedArtistId).FirstOrDefault();
				artists.Add(artist);
			}




			return artists;
		}


		// PUT: api/Follows/5
		//[HttpPut("{id}")]
		//public async Task<IActionResult> PutFollow([FromRoute] int id, [FromBody] Follow follow)
		//{
		//    if (!ModelState.IsValid)
		//    {
		//        return BadRequest(ModelState);
		//    }

		//    if (id != follow.Id)
		//    {
		//        return BadRequest();
		//    }

		//    _context.Entry(follow).State = EntityState.Modified;

		//    try
		//    {
		//        await _context.SaveChangesAsync();
		//    }
		//    catch (DbUpdateConcurrencyException)
		//    {
		//        if (!FollowExists(id))
		//        {
		//            return NotFound();
		//        }
		//        else
		//        {
		//            throw;
		//        }
		//    }

		//    return NoContent();
		//}

		// POST: api/Follows
		[HttpPost]
		public async Task<IActionResult> PostFollow([FromBody] Follow follow)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.Follow.Add(follow);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetFollow", new { id = follow.Id }, follow);
		}

		// DELETE: api/Follows/5
		[HttpDelete("{FollowingUserId}")]
        public async Task<IActionResult> DeleteFollow([FromRoute] string FollowingUserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var follow = await _context.UserFollow.SingleOrDefaultAsync(m => m.FollowedUserId == FollowingUserId);
            if (follow == null)
            {
                return NotFound();
            }

            _context.UserFollow.Remove(follow);
            await _context.SaveChangesAsync();

            return Ok(follow);
        }

        private bool FollowExists(int id)
        {
            return _context.Follow.Any(e => e.Id == id);
        }
    }
}