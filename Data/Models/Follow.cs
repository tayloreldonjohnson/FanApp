using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Hello.Data.Models
{
    public class Follow
    {
        //[Key]
        //public int FollowId { get; set; }
        //other properties
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string FollowerId { get; set; }
        public ApplicationUser User { get; set; }



        [ForeignKey("FollowedArtistId")]
        public int FollowedArtistId { get; set; }
        public ApplicationArtist FollowedArtist { get; set; }
        public DateTime DateCreated { get; set; }
        //[ForeignKey("FollowedUserId")]
        //public  ApplicationUser FollowedUserId { get; set; }
    }
}