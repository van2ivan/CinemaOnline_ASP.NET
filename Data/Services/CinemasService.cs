using CinemaOnline.Data.Base;
using CinemaOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaOnline.Data.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(ApplicationDbContext context) : base(context)
        {

        }
    }
}
