using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Data.Models
{
    public class ApplicationArtist
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Genre { get; set; }
            public string ImageUrl { get; set; }
        
			//public ICollection<UserPost> UserPosts { get; set; }
			public List<Post> Posts { get; set; }
    }
}
