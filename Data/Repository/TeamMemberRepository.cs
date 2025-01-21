using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private readonly ApplictionDbContext _context;

        public TeamMemberRepository(ApplictionDbContext context)
        {
            _context = context;
        }

        //Adds a new teamMember [returns teamMember id]
        public string AddTeamMember(TeamMember teamMember)
        {
            _context.TeamMember.Add(teamMember);
            _context.SaveChanges();
            return ($"Team member " + teamMember.DeveloperID + " added to team " + teamMember.TeamID);
        }

        //Update team details also used for soft delete[does not return anything]
        public void UpdateTeamMember(TeamMember teamMember)
        {
            _context.TeamMember.Update(teamMember);
            _context.SaveChanges();
        }

        //Hard deleting teamMember
        public void DeleteTeamMemebr(int teamID, int developerID)
        {
            TeamMember teamM = _context.TeamMember.FirstOrDefault(t => t.DeveloperID == developerID && t.TeamID == teamID);
            _context.TeamMember.Remove(teamM);
            _context.SaveChanges();

        }

        //Gets all teamMembers [returns list of teamMembers]
        public List<TeamMember> GetAllTeamMembers()
        {
            return _context.TeamMember.ToList();
        }

        //Get by Developer ID [returns teamMember]
        public TeamMember GetTeamByDeveloperID(int DevID)
        {
            return _context.TeamMember.FirstOrDefault(t => t.DeveloperID == DevID);
        }

        //Get by Team ID [returns teamMember]
        public TeamMember GetTeamByTeamID(int TeamID)
        {
            return _context.TeamMember.FirstOrDefault(t => t.TeamID == TeamID);
        }

        //Counts how many team slots are taken
        public int GetNoTakenSlots(int TeamID)
        {
            return _context.TeamMember.Count(t => t.TeamID == TeamID);
        }

        //checks that this team member is part of this team 
        public bool CheckTMinTeam(int TeamID, int devID)
        {
            return _context.TeamMember.Any(t => t.TeamID == TeamID && t.DeveloperID == devID);
        }

        //Get TeamMembers by teamID [returns teamMember]
        public List<TeamMember> GetTeamMemberByTeamID(int TeamID)
        {
            return _context.TeamMember.Where(t => t.TeamID == TeamID).ToList();
        }
    }
}
