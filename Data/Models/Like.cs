using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Data.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
   

        [ForeignKey("ApplicationUserId")]  
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        [ForeignKey("PostId")]

        public string PostId { get; set; }

        public Post post { get; set; }

        public DateTime DateLiked { get; set; }
    }
}
