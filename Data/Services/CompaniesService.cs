using CinemaOnline.Data.Base;
using CinemaOnline.Models;
using CompanyOnline.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace CinemaOnline.Data.Services
{
    public class CompaniesService : EntityBaseRepository<Company>, ICompaniesService
    { 
        public CompaniesService(ApplicationDbContext context) : base(context)
        {

        }
    }
}
