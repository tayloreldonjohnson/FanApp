using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hello.Data.Models
{
	public class Artist
	{
		public string Name { get; set; }
		//public string PlayCount { get; set; }
		//public string Listeners { get; set; }
		//public string mbid { get; set; }
		//public string Url { get; set; }
		//public bool Streamable { get; set; }
		public List<Image> Image { get; set; }
	}
	public class Image
	{
		[JsonProperty("#Text")]
		public string Text { get; set; }
		public string Size { get; set; }
	}
	public class Artists
	{
		public List<Artist> Artist { get; set; }
	}
	public class ArtistParent
	{
		public Artists Artists { get; set; }
	}
}
