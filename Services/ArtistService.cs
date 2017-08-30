using Hello.Data;
using Hello.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.Services
{
    namespace Hello.Services
    {
        public class ArtistService : DbContext
        {


            public ApplicationDbContext _db;
            public ArtistService(ApplicationDbContext db)
            {

                _db = db;
            }
            public List<ApplicationArtist> GetAllArtists()
            {
                var users = _db.ApplicationArtist.ToList();

                return users;
            }
            public ArtistVM GetArtist(string name)
            {

                var artist = _db.ApplicationArtist.Where(m => m.Name == name).FirstOrDefault();
                var newArtist = new ArtistVM
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    Genre = artist.Genre,
                    ImageUrl = artist.ImageUrl

                };

                return newArtist;

            }

			

        }
    }
}