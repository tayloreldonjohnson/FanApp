using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Data.Models
{
    public class Inbox
    {

        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("ApplicationUserId")]


        public string MessagerUserId { get; set; }
        public ApplicationUser MessagerUser { get; set; }
        [ForeignKey("ApplicationUserId")]


        public string RecieverOfMessageId { get; set; }
        public ApplicationUser RecieverOfMessage { get; set; }
    }
}
 