using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface ITeamService
    {
        IEnumerable<Team> GetAll();
    }
    public class TeamService : ITeamService
    {
        private DataContext _context;

        public TeamService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Team> GetAll()
        {
            return _context.Teams;
        }
    }
}
