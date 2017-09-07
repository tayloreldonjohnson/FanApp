using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hello.Data;
using Hello.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hello.Controllers
{
	
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
		public ApplicationDbContext _db;
		public ValuesController( ApplicationDbContext db)
		{
			_db = db;
		}
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public List<Post> Get(int id)
        {
			var posts = _db.Post.Where(u => u.ApplicationArtistId == id).ToList();
			return posts;
		}

		// POST api/values
		//[HttpPost]
		//public string Post([FromBody]string data)
		//{
		//	foreach (var artist in artistCon.Artist)
		//	{
		//		var newartist = new ApplicationArtist
		//		{
		//			Name = artist.Name,
		//			ImageUrl = artist.Image[4]
		//		};
		//		_db.Add(newartist);
		//	}
		//}

		// POST api/values
		//[HttpPost]
		//public void Post([FromBody]ArtistParent artistsP)
		//{
		//	foreach (Artist artist in artistsP.Artists.Artist)
		//	{
		//		var newArtist = new ApplicationArtist
		//		{
		//			Name = artist.Name,
		//			//ImageUrl = artist.Image[4].Text
		//		};
		//		_db.Add(newArtist);
		//	}
		//}

		// PUT api/values/5
		[HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
