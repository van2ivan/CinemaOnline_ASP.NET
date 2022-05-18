using CinemaOnline.Models;
using Microsoft.EntityFrameworkCore;
using CinemaOnline.Data.Base;

namespace CinemaOnline.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(ApplicationDbContext context) : base(context)
        {

        }
    }
}
