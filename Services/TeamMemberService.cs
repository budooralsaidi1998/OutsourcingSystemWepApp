using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;

namespace OutsourcingSystemWepApp.Services
{
    public class TeamMemberService : ITeamMemberService
    {

        private readonly ITeamMemberRepository _teammemberRepository;
        public TeamMemberService(ITeamMemberRepository teammemberrepo)
        {
            _teammemberRepository = teammemberrepo;
        }

        //Adds teamMember using input from user 
        public string AddTeamMember(int teamID, int developerID)
        {
            //mapping input to teamMember 
            var teamMember = new TeamMember
            {
                TeamID = teamID,
                DeveloperID = developerID,
                JoinedAt = DateTime.Now,
            };

            return _teammemberRepository.AddTeamMember(teamMember);
        }

        //Deleting teamMember [returns 0 no errors or 1 error occured]
        public int DeleteTeamMember(int TeamID, int DeveloperID)
        {
            try
            {
                _teammemberRepository.DeleteTeamMemebr(TeamID, DeveloperID);
                return 0; //no errors
            }
            catch { return 1; } //error occured 
        }

        //Updates teamMember using input from user [returns 0 no errors or 1 error occured]
        public int UpdateTeamMember(int teamID, int developerID)
        {
            try
            {
                //mapping input to teamMember
                var teamMember = new TeamMember
                {
                    TeamID = teamID,
                    DeveloperID = developerID,
                    JoinedAt = DateTime.Now,
                };

                _teammemberRepository.UpdateTeamMember(teamMember);
                return 0; //no error
            }
            catch { return 1; }
        }


        public List<TeamMember> GetAllTeamMembers(int Page, int PageSize, int? developerID, int? teamID)
        {
            var teamMembers = _teammemberRepository.GetAllTeamMembers();

            // Filters by if developerID if provided 
            if (developerID.HasValue)
            {
                teamMembers = teamMembers.Where(t => t.DeveloperID == developerID).ToList();
            }

            // Filters by teamID at if provided 
            if (teamID.HasValue)
            {
                teamMembers = teamMembers.Where(t => t.TeamID == teamID).ToList();
            }

            // Paginating results and returning 
            int number = PageSize * Page;
            return teamMembers.OrderBy(t => t.TeamID).Skip(number).Take(PageSize).ToList();
        }

        //Counts how many team slots are taken
        public int GetNoTakenSlots(int TeamID)
        {
            return _teammemberRepository.GetNoTakenSlots(TeamID);
        }

        public List<TeamMember> GetTeamMemberByTeamID(int TeamID)
        {
            return _teammemberRepository.GetTeamMemberByTeamID(TeamID);
        }

        public bool CheckTMinTeam(int TeamID, int devID)
        {
            var teamFound = _teammemberRepository.CheckTMinTeam(TeamID, devID);
            return teamFound == true ? true : false;
        }

        //returns team ID if found
        public int IsDevInTeam(int devID)
        {
            TeamMember teamM = _teammemberRepository.GetTeamByDeveloperID(devID);
            return teamM == null ? 0 : teamM.TeamID;
        }
    }
}
