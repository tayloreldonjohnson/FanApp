using Hello.Data;
using Hello.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Services
{
    public class PostService
    {
        public ApplicationDbContext _db;
        public PostService( ApplicationDbContext db)
        {
            
            _db = db;
        }



        public void AddPost(Post post)
        {

            //         var IntendedUser = _uManager.Users.Where(m => m.Email == user.Email).FirstOrDefault();
            //         IntendedUser.AboutMe = user.AboutMe;
            //IntendedUser.ImageUrl = user.ImageUrl;
            //         await _uManager.UpdateAsync(IntendedUser);
            // _db.SaveChanges();

            //return;
            var postPics = new Post
            {
                ApplicationArtistId = post.ApplicationArtistId,
                ApplicationUserId = post.ApplicationUserId,
                Media = post.Media,
                DateCreated = post.DateCreated
            };
            
    
            //user.AboutMe = postVm.AboutMe;
           
            _db.Add(postPics);
            _db.SaveChanges();
        }

    }
}
