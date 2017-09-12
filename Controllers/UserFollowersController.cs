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
        public class UserPostVm
        {
            public int PostId { get; set; }
            public  int ApplicationArtistId { get; set; }
            public string FirstNameOfPersonWhoPosted { get; set; }
            public string LastNameOfPersonWhoPosted { get; set; }

            public string ArtistName { get; set; }
            public   string BeingFollowedId { get; set; }
            public DateTime DateCreated { get; set; }
            public string media { get; set; }
            public string Video { get; set; }
            public string Caption { get; set; }
            public string  ProfileImage { get; set; }


        }

        [HttpGet("postandprofile/{id}")]
        //public List<Post> GetFollowedPost(string id)
        public List <UserPostVm> GetPostWithProfile(string id)
        {
            var newpost = new UserPostVm();
            var allPosts = new List<UserPostVm>();
            var usersBeingFollowed= _context.UserFollow.Where(uf => uf.FollowingUserId == id).ToList();
            //var followingYou = _context.UserFollow.Where(uf => uf.FollowedUserId == id).ToList();
      
           
            foreach (var uf in usersBeingFollowed)
            {
                var user = _context.ApplicationUser.Where(u => u.Id == uf.FollowedUserId).FirstOrDefault();
                var posts = _context.Post.Where(u => u.ApplicationUserId == uf.FollowedUserId).ToList();
                var userWithPosts = new UserPostVm()
                {
                    BeingFollowedId = uf.FollowedUserId,
                    ProfileImage = user.ImageUrl,
                    FirstNameOfPersonWhoPosted = user.FirstName,
                    LastNameOfPersonWhoPosted = user.LastName


                };
                
                foreach(var posted in posts)
                {
                    foreach (var artist in posts)
                    {
                        var artistinfo = _context.ApplicationArtist.Where(m => m.Id == posted.ApplicationArtistId).FirstOrDefault();
                        userWithPosts.ArtistName = artistinfo.Name;
                        userWithPosts.ApplicationArtistId = artistinfo.Id;
                    }

                    userWithPosts.PostId = posted.PostId;
                    userWithPosts.media = posted.Media;
                    userWithPosts.Video = posted.Video;
                    userWithPosts.Caption = posted.caption;
                    userWithPosts.DateCreated = posted.DateCreated;

                }
                allPosts.Add(userWithPosts);
            
            }
          
            return allPosts;
        }

        //----------------TEST------------------TEST--------TEST------------TEST--------------------------------------------------------------------






        public class PostsFollowDataVM
        {
            public List<Post> Posts { get; set; }
            public int NumberOfFollowers { get; set; }
            public int NumberOfFollowing { get; set; }
           
      
        }

        [HttpGet("{id}")]
        //public List<Post> GetFollowedPost(string id)
        public PostsFollowDataVM GetFollowedPost(string id)
        {
            var data = new PostsFollowDataVM();
            var allPosts = new List<Post>();
            var YouFollow = _context.UserFollow.Where(uf => uf.FollowingUserId == id).ToList();
            var followingYou = _context.UserFollow.Where(uf => uf.FollowedUserId == id).ToList();
            var countOfYourFollowers = followingYou.Count();
            var NumberOfPeopleYouFollow = YouFollow.Count();
        
                
            foreach (var user in YouFollow)
            {
                var postList = _context.Post.Where(p => p.ApplicationUserId == user.FollowedUserId).ToList();

                foreach (var post in postList)
                {
                    allPosts.Add(post);
                }

            }
            data.Posts = allPosts;
           
            data.NumberOfFollowers = countOfYourFollowers;
            data.NumberOfFollowing = NumberOfPeopleYouFollow;
            return data;
        }

        [HttpGet("count/{id}")]
        //public List<Post> GetFollowedPost(string id)

        public int Getcountoffollowing(string id)
        {
          
            var users = _context.UserFollow.Where(uf => uf.FollowingUserId == id).ToList();
            var count = users.Count();
      
            return count;
        }

        [HttpGet("followers/{id}")]
        public int Getcountoffollowers(string id)
        {

            var users = _context.UserFollow.Where(uf => uf.FollowedUserId == id).ToList();
            var count = users.Count();

            return count;
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