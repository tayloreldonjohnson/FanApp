using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Data.Models
{
    public class PostVM
    {

        
            public int PostId  { get; set; }
            public int ApplicationArtistId { get; set; }
            public string ApplicationUserId { get; set; }
        
            public DateTime DateCreated { get; set; }
         
            public string media{ get; set; }

        
    }
}
