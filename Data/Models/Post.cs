using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Data.Models
{
	//  public class Post
	//  {
	//[Key]
	//public int PostId { get; set; }
	////other properties
	//public string Media { get; set; }
	//public DateTime DateCreated { get; set; }

	//public ICollection<UserPost> UserPost { get; set; }
	//public ICollection<ArtistPost> ArtistPost { get; set; }
	//  }
	public class Post
	{
		[Key]
		public int PostId { get; set; }
		//other properties
		public string Media { get; set; }
		public DateTime DateCreated { get; set; }
		[ForeignKey("ApplicationUserId")]
		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationArtistId")]
		public int ApplicationArtistId { get; set; }
	}
}
