using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hello.Data;
using Hello.Data.Models;
using Hello.Services;
using System.Net;
using System.IO;

namespace Hello.Controllers
{
    [Produces("application/json")]
    [Route("api/Posts")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        //public PostService  _postService;
        //public PostsController(PostService postService)
        //{
        //}

        public PostsController(ApplicationDbContext context)
        {
            //_postService = postService;
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public IEnumerable<Post> GetPost()
        {
            return _context.Post;
        }

		//GET: api/Posts/5
		// Gets Posts for Artist by ArtistId
		[HttpGet("{id:int}")]
		public List<Post> Get(int id)
		{
			var posts = _context.Post.Where(u => u.ApplicationArtistId == id).ToList();
			return posts;
		}

        // Added to get amount of uploads will change how you get posts----------------------------------------------------------------------------------------------------------
        public class PostsUploadedDataVM
        {
            public List<Post> Posts { get; set; }
            public int NumberOfPosts { get; set; }
        
        }

        [HttpGet("numberOfPosts/{id}")]
        public PostsUploadedDataVM GetUserPosts(string id)
        {
            var data = new PostsUploadedDataVM();
            //var allPosts = new List<Post>();
            var userPosts = _context.Post.Where(u => u.ApplicationUserId == id).ToList();
            var NumberOfPosts = userPosts.Count();
            data.Posts = userPosts;
            data.NumberOfPosts = NumberOfPosts;
            return data;
        }
        // ------------------------------------------------------------------------------------------------------------------------------------------
        // Gets User post by UserId
        [HttpGet("{id}")]
		public List<Post> GetUserId(string id)
		{
			var userposts = _context.Post.Where(u => u.ApplicationUserId == id).ToList();
			return userposts;
		}

		//[HttpGet("{ApplicationArtistid}")]
		//public async Task<IActionResult> GetPostId([FromRoute] int ApplicationArtistid)
		//{
		//    if (!ModelState.IsValid)
		//    {
		//        return BadRequest(ModelState);
		//    }
		//    var Userpost = await _context.Post.SingleOrDefaultAsync(m => m.ApplicationArtistId == ApplicationArtistid);

		//    if (Userpost == null)
		//    {
		//        return NotFound();
		//    }

		//    return Ok(Userpost);
		//}

		//[HttpGet("{id}")]
		//public async Task<IActionResult> GetPost([FromRoute] int id)
		//{
		//    if (!ModelState.IsValid)
		//    {
		//        return BadRequest(ModelState);
		//    }

		//    var post = await _context.Post.SingleOrDefaultAsync(m => m.PostId == id);

		//    if (post == null)
		//    {
		//        return NotFound();
		//    }

		//    return Ok(post);

		//}

		// PUT: api/Posts/5
		[HttpPut("{id}")]
        public async Task<IActionResult> PutPost([FromRoute] int id, [FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.PostId)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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


        //   [HttpPost]
        //public string Post([FromBody] Post post)
        //{
        //    try
        //    {
        //        _postService.AddPost(post);
        //        return "Success!";
        //    }
        //    catch
        //    {
        //        return "Fail! user was not updated";
        //    }
        //  }

        //[HttpPost("{ApplicationArtistId}")]
        [HttpPost]
      
        public async Task<IActionResult> PostMedia([FromBody] Post post)
        {

          //  try
          //  {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Post.Add(post);
                await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.PostId }, post);
            //}
            //catch (WebException webex)
            //{
            //    WebResponse errResp = webex.Response;
            //    using (Stream respStream = errResp.GetResponseStream())
            //    {
            //        StreamReader reader = new StreamReader(respStream);
            //        string text = reader.ReadToEnd();
            //    }
            //}

        }
        //   POST: api/Posts
        //[HttpPost]
        //public async Task<IActionResult> PostPost([FromBody] Post post)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

            //    _context.Post.Add(post);
            //    await _context.SaveChangesAsync();

            //    return CreatedAtAction("GetPost", new { id = post.PostId }, post);
            //}

            // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Post.SingleOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.PostId == id);
        }
    }
}