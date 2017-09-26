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
        //[HttpGet("{id:int}")]
        //public List<Post> Get(int id)
        //{
        //	var posts = _context.Post.Where(u => u.ApplicationArtistId == id).ToList();
        //	return posts;
        //}




        [HttpGet("artist/{id}")]
        public List<PostsLikedDataVM> GetPostsWithLikesbyArtist(int id)
        {
            var userposts = _context.Post.Where(u => u.ApplicationArtistId == id).ToList();


            var PostsLiked = new List<PostsLikedDataVM>();
            foreach (var post in userposts)
            {
                var userLikes = _context.Like.Where(u => u.PostId == post.PostId).ToList();
                var user = _context.ApplicationUser.Where(ui => ui.Id == post.ApplicationUserId).FirstOrDefault();
                var PostswithLikes = new PostsLikedDataVM
                {
                    ProfileImage = user.ImageUrl,
                    UserName = user.UserName,
                    PostId = post.PostId,
                    Media = post.Media,
                    Caption = post.caption,
                    Video = post.Video,
                    DateCreated = post.DateCreated,

                };
                foreach (var like in userLikes)
                {
                    var NumberOfLikes = userLikes.Count();
                    PostswithLikes.NumberofLikes = NumberOfLikes;
                }


                PostsLiked.Add(PostswithLikes);
            }


            return PostsLiked;
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
        public class PostsLikedDataVM
        {

            public int ApplicationArtistId { get; set; }
            public string UserName { get; set; }
            public int PostId { get; set; }

            public string ArtistName { get; set; }
            public string UserId { get; set; }
            public DateTime DateCreated { get; set; }
            public string Media { get; set; }
            public string Video { get; set; }
            public string Caption { get; set; }
            public string ProfileImage { get; set; }
            public string Text { get; set; }
            public int NumberofLikes { get; set; }

        }

        [HttpGet("Likes/{id}")]
        public List<PostsLikedDataVM> GetPostsWithLikes(string id)
        {
            var userposts = _context.Post.Where(u => u.ApplicationUserId == id).ToList();

           
            var PostsLiked = new List<PostsLikedDataVM>();
            foreach (var post in userposts)
            {
                var userLikes = _context.Like.Where(u => u.PostId == post.PostId).ToList();
            
                var PostswithLikes = new PostsLikedDataVM {
                PostId = post.PostId,
                Media = post.Media,
                Caption = post.caption,
               Video = post.Video,
               DateCreated = post.DateCreated,

            };
                foreach (var like in userLikes)
                {
                    var NumberOfLikes = userLikes.Count();
                    PostswithLikes.NumberofLikes = NumberOfLikes;
                }
            

               PostsLiked.Add(PostswithLikes);
            }

          
            return PostsLiked;
        }
        // ------------------------------------------------------------------------------------------------------------------------------------------
        // Gets User post by UserId
        [HttpGet("{id}")]
		public List<Post> GetUserId(string id)
		{
			var userposts = _context.Post.Where(u => u.ApplicationUserId == id).ToList();


          
            return userposts;
		}

        public class postTypeVm {

            public string Type{ get; set; }
            public string Id { get; set; }


        }
        [HttpGet("getType/{id}")]
        public List<postTypeVm> GetType(string id)
        {
            var userposts = _context.Post.Where(u => u.ApplicationUserId == id).ToList();
            var listOfTypes = new List<postTypeVm>();
            foreach (var post in userposts)
            {
                var TypesOfPosts = new postTypeVm
                {
                    Type = Path.GetExtension(post.Media)
                };

                listOfTypes.Add(TypesOfPosts);
           }

            return listOfTypes;
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

        }
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