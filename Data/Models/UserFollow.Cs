using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Data.Models
{
    public class UserFollow
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUserId")]

        //Person who is following
        public string FollowingUserId { get; set; }
        public ApplicationUser FollowingUser { get; set; }
        [ForeignKey("ApplicationUserId")]


        public string FollowedUserId { get; set; }
        public ApplicationUser FollowedUser { get; set; }
    }
}
