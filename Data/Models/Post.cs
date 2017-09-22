using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Data.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        //other properties
        public string Media { get; set; }
        public string Video { get; set; }
        public string caption { get; set; }
        public DateTime DateCreated { get; set; }
        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationArtistId")]
        public int ApplicationArtistId { get; set; }
        public ICollection<Comment> Comment {get; set;}
        public ICollection<Like> Like {get; set;}
	}
}
