using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Data.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public int PostId { get; set; }
        public Post post { get; set; }
        [ForeignKey("ApplicationUserId")]
        public string UserId { get; set; }
   
        public ApplicationUser User { get; set; }
        public string Text { get; set; }
    }
}
