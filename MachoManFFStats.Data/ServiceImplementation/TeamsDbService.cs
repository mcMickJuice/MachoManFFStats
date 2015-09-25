using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MachoManFFStats.BLL;
using MachoManFFStats.BLL.Models;
using MachoManFFStats.Data.Entity;

namespace MachoManFFStats.Data.ServiceImplementation
{
    public class TeamsDbService : ITeamsService
    {
        private readonly MachoManFFEntities _context;

        public TeamsDbService(MachoManFFEntities context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            var teams = await getTeamsByPredicateAsync();

            return teams;
        }

        public async Task<IEnumerable<Team>> GetActiveTeams()
        {
            var activeTeams = await getTeamsByPredicateAsync(m => m.IsActive);

            return activeTeams;
        }

        public async Task<IEnumerable<FfTeam>> GetFantasyTeams_ForYear(int year)
        {
            var fantasyTeams = await _context.FFTeams
                .Where(t => t.Year == year)
                .Select(t => new FfTeam
                {
                    EspnTeamId = t.ESPNFFTeamID,
                    Year = t.Year,
                    TeamName = t.Team
                })
                .ToListAsync();

            return fantasyTeams;
        }

        public async Task<TeamProfile> GetTeamProfileById(int id)
        {
            var dbTeamProfile = await _context.Members
                .Include(m => m.FFTeams)
                .FirstOrDefaultAsync(m => m.MemberID == id);

            if (dbTeamProfile == null)
            {
                return null;
            }

            var team = new TeamProfile
            {
                MemberId = dbTeamProfile.MemberID,
                MemberName = dbTeamProfile.MemberName,
                FantasyTeams = dbTeamProfile.FFTeams.Select(t => new FfTeam
                {
                    EspnTeamId = t.ESPNFFTeamID,
                    TeamName = t.Team,
                    Year = t.Year
                })
            };

            return team;
        }

        private async Task<IEnumerable<Team>> getTeamsByPredicateAsync(Expression<Func<Member, bool>> predicate = null)
        {
            var query = _context.Members.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.Select(t => new Team
            {
                MemberName = t.MemberName,
                MemberId = t.MemberID
            })
                .ToListAsync();
        }
    }
}
