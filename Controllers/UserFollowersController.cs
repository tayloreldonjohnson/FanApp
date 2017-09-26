using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hello.Data;
using Hello.Data.Models;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

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
//------------------------------------------------------------------Testing below this-------------------------------------------------------
        [HttpGet("postbyemail/{email}")]
        public List<UserPostVm> getbyemailProfile(string email)
        {

            var allPosts = new List<UserPostVm>();

            //get a list of people being followed by the user
            var userid = "";
            var listOfUsers = _context.ApplicationUser.ToList();
            var listOfFollowers = _context.UserFollow.ToList();
            foreach (var user in listOfUsers)
            {
                user.Id = userid;
            }
            var useridOfFollowers = _context.ApplicationUser.Where(uf => uf.Email == email).ToList();
            var usersBeingFollowed = _context.UserFollow.Where(uf => uf.FollowingUserId == userid).ToList();
            var followers = _context.UserFollow.Where(uf => uf.FollowedUserId == userid).ToList();

            //loop through list of people being followed
            foreach (var uf in usersBeingFollowed)
            {


                // gets all posts of all people being followed
                var posts = _context.Post.Where(u => u.ApplicationUserId == uf.FollowedUserId).ToList();
                //go through the posts and get info about the post 
                foreach (var post in posts)
                {
                    var comment = _context.Comment.Where(ui => ui.PostId == post.PostId).FirstOrDefault();
                    var userLikes = _context.Like.Where(u => u.PostId == post.PostId).ToList();

                    var user = _context.ApplicationUser.Where(ui => ui.Id == post.ApplicationUserId).FirstOrDefault();
                    var artist = _context.ApplicationArtist.Where(ai => ai.Id == post.ApplicationArtistId).FirstOrDefault();
                    var UserFollowedInfo = new UserPostVm()
                    {
                        UserId = user.Id,
                        ProfileImage = user.ImageUrl,
                        ArtistName = artist.Name,
                        PostId = post.PostId,
                        Media = post.Media,
                        Video = post.Video,
                        Caption = post.caption,
                        DateCreated = post.DateCreated,
                        UserName = user.UserName,
                        ApplicationArtistId = artist.Id,
                        Text = comment.Text
                        
                    };
                    foreach (var like in userLikes)
                    {
                        var NumberOfLikes = userLikes.Count();
                        UserFollowedInfo.NumberofLikes = NumberOfLikes;
                    }

                    allPosts.Add(UserFollowedInfo);
                }

            }

            return allPosts;
        }
//---------------------------------------------------------Testing above this------------------------------------------------------------------
        // GET: api/UserFollowers
        [HttpGet]
        public IEnumerable<UserFollow> GetUserFollower()
        {
            return _context.UserFollow;
        }

        public class UserPostVm
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
            public string  Text { get; set; }
            public int NumberofLikes { get; set; }


        }

        [HttpGet("postandprofile/{id}")]
        //public List<Post> GetFollowedPost(string id)
        public List<UserPostVm> GetPostWithProfile(string id)
        {

            var allPosts = new List<UserPostVm>();

            //get a list of people being followed by the user
            var usersBeingFollowed = _context.UserFollow.Where(uf => uf.FollowingUserId == id).ToList();
            var followers = _context.UserFollow.Where(uf => uf.FollowedUserId == id).ToList();

            //loop through list of people being followed
            foreach (var uf in usersBeingFollowed)
            {


                // gets all posts of all people being followed
                var posts = _context.Post.Where(u => u.ApplicationUserId == uf.FollowedUserId).ToList();
                  
                //go through the posts and get info about the post 
                foreach (var post in posts)
                {

                    var comment = _context.Comment.Where(ui => ui.PostId == post.PostId  ).FirstOrDefault();
                   var userLikes = _context.Like.Where(u => u.PostId == post.PostId).ToList();

                   
                    var user = _context.ApplicationUser.Where(ui => ui.Id == post.ApplicationUserId).FirstOrDefault();
                    var artist = _context.ApplicationArtist.Where(ai => ai.Id == post.ApplicationArtistId).FirstOrDefault();
                    var UserFollowedInfo = new UserPostVm()
                    {
                        UserId = user.Id,
                        ProfileImage = user.ImageUrl,
                        ArtistName = artist.Name,
                        PostId = post.PostId,
                        Media = post.Media,
                        Video = post.Video,
                        Caption = post.caption,
                        DateCreated = post.DateCreated,
                        UserName = user.UserName,
                        ApplicationArtistId = artist.Id,
                      

                    };
                    foreach (var like in userLikes)
                    {
                        var NumberOfLikes = userLikes.Count();
                        UserFollowedInfo.NumberofLikes = NumberOfLikes;
                    }


                    allPosts.Add(UserFollowedInfo);
                }

            }

            return allPosts;
        }

        //----------------TEST------------------TEST--------TEST------------TEST--------------------------------------------------------------------
        public class PostsFollowDataVM
        {
            public List<Post> Posts { get; set; }
            public int NumberOfFollowers { get; set; }
            public int NumberOfFollowing { get; set; }
            public List <int> NumberOfLikes { get; set; }


        }

        [HttpGet("{id}")]
        //public List<Post> GetFollowedPost(string id)
        public PostsFollowDataVM GetFollowedPost(string id)
        {
            var data = new PostsFollowDataVM();
            var allPosts = new List<Post>();
            var allLikes = new List<int>();
            var YouFollow = _context.UserFollow.Where(uf => uf.FollowingUserId == id).ToList();
            var followingYou = _context.UserFollow.Where(uf => uf.FollowedUserId == id).ToList();
            var countOfYourFollowers = followingYou.Count();
            var NumberOfPeopleYouFollow = YouFollow.Count();
         

            foreach (var user in YouFollow)
            {
                var postList = _context.Post.Where(p => p.ApplicationUserId == user.FollowedUserId).ToList();
               
                //loop through posts
                foreach (var post in postList)
                {
                    // get a list of all the 

                    //var postLikes = _context.Like.Where(u => u.PostId == post.PostId).GroupBy(lk => lk.PostId).Select(g => g.Count(x => x.LikeId )).FirstOrDefault();
                    var postLikes = _context.Like.Where(lk => lk.PostId == post.PostId)
                             .GroupBy(n => n.PostId)
                              .Select(p => p.Count()
                              

                                 
                              );
 

                     allPosts.Add(post);
                  
                   
                }

            }
            data.Posts = allPosts;
           
            data.NumberOfFollowers = countOfYourFollowers;
            data.NumberOfFollowing = NumberOfPeopleYouFollow;
            return data;
        }
        //  ----------------------------

        [HttpPost]
        public async Task<IActionResult> PostUserFollowerWithNoDuplicates([FromBody] UserFollow userFollow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var duplicates = _context.UserFollow.Where(uf => uf.FollowedUserId == userFollow.FollowedUserId && uf.FollowingUserId == userFollow.FollowingUserId).Count();

            if (duplicates > 0 || userFollow.FollowedUserId == userFollow.FollowingUserId)
            {

                var error = new
                {
                    message = "You are either following this person or attempting to follow yourself",
                    status = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError
                };
                //Context.Response.StatusCode = error.status;
                return new ObjectResult(error);


            }

            _context.UserFollow.Add(userFollow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserFollower", new { id = userFollow.Id }, userFollow);
        }



        //----------------------------------
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
        /////------------------------------------------------------------------

        //--------------------------------------------
        // GET: api/UserFollowers/5
        //[HttpGet("{FollowedUserId}")]
        //public async Task<IActionResult> GetUserFollowerinfo( [FromRoute]  string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var userFollower = await _context.UserFollow.SingleOrDefaultAsync(m => m.FollowedUserId == id  );

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
        //public class IsFollowed {

        //    public bool isFollowed { get; set; }
        //}
        [HttpGet("isFollowing/{followedid}/{followerid}")]
        public  bool DetermineIfFollowed(string followedid, string followerid){
            var isFollowed = true;
            //var users = _context.UserFollow.Where(uf => uf.FollowingUserId == followerid &&  uf.FollowedUserId == followedid).ToList();

            if (_context.UserFollow.Any(u => u.FollowingUserId == followerid && u.FollowedUserId == followedid))
            {
                isFollowed = true;
            }
            else
            {
                isFollowed = false;
            }
            return isFollowed;
        }

        // POST: api/UserFollowers
        //[HttpPost]
        //public async Task<IActionResult> PostUserFollower([FromBody] UserFollow userFollow)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

            //    _context.UserFollow.Add(userFollow);
            //    await _context.SaveChangesAsync();

            //    return CreatedAtAction("GetUserFollower", new { id = userFollow.Id }, userFollow);
            //}

            // DELETE: api/UserFollowers/5
        [HttpDelete("unfollow/{followedid}/{followingid}")]
		public async Task<IActionResult> DeleteUserFollower([FromRoute] string followedid, string followingid)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var userFollower = await _context.UserFollow.SingleOrDefaultAsync(m => m.FollowedUserId == followedid && m.FollowingUserId == followingid);
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