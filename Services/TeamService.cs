using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;

namespace OutsourcingSystemWepApp.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        public TeamService(ITeamRepository teamrepo)
        {
            _teamRepository = teamrepo;
        }

        //Adds team using input from user 
        public int AddTeam(int adminID, TeamInDTO input)
        {
            //mapping TeamInDTO to team 
            var team = new Team
            {
                TeamName = input.TeamName,
                Description = input.Description,
                TeamCapacity = input.TeamCapacity,
                HourlyRate = input.HourlyRate,
                ModifiedBy = adminID
                //other values will be set to their defualts
            };

            return _teamRepository.AddTeam(team);
        }

        //Soft deleting team
        public int DeleteTeam(int TeamID)
        {
            try
            {
                var team = _teamRepository.GetTeamByID(TeamID);

                //Checking if the team exists
                if (team != null)
                {
                    team.Active = false; //deactivating team

                    _teamRepository.UpdateTeam(team);

                    return 0; //if everything went well then 1 will be returned 
                }

                else return 1; //team not found
            }
            catch { return 2; } //an error occured when trying to save
        }

        //Checks if the team exists
        public bool CheckTeamByID(int TeamID)
        {
            var team = _teamRepository.GetTeamByID(TeamID);
            return team != null ? true : false; //if team found return true if not found return false
        }


        public  List<Team> GetTeamsBasedOnSearchValue(string Value)
        {
            var value = Value.ToLower();
            var teams = _teamRepository.GetAllTeams();
            return teams.Where(t=>t.TeamName.ToLower().Contains(value) || t.Description.ToLower().Contains(value)).ToList();
        }

        public Team GetTeamByID(int TeamID)
        {
            return _teamRepository.GetTeamByID(TeamID);
        }

        //Reactivating after soft delete
        public int ReactivateTeam(int TeamID)
        {
            try
            {
                var team = _teamRepository.GetTeamByID(TeamID);

                //Checking if the team exists
                if (team != null)
                {
                    team.Active = true; //reactivating team

                    _teamRepository.UpdateTeam(team);

                    return 0; //if everything went well then 1 will be returned 
                }

                else return 1; //team not found
            }
            catch { return 2; } //an error occured when trying to save
        }


        //Adds team using input from user 
        public int UpdateTeam(int TeamID, int AdminID, TeamUpdateDTO team)
        {
            var oldTeam = _teamRepository.GetTeamByID(TeamID);

            //Makes sure that only inputted data is changed 
            if (oldTeam != null)
            {
                if (team.TeamCapacity.HasValue)
                { if (team.TeamCapacity != 2147483647) oldTeam.TeamCapacity = (int)team.TeamCapacity; }


                if (team.HourlyRate.HasValue)
                { if (team.HourlyRate != 0) oldTeam.HourlyRate = (decimal)team.HourlyRate; }

                if (team.TeamName != "string")
                { oldTeam.TeamName = team.TeamName; }

                if (team.Description != "string")
                { oldTeam.Description = team.Description; }


                //mapping TeamInDTO to team 
                oldTeam.ModifiedAt = DateTime.Now;
                oldTeam.ModifiedBy = AdminID;

                _teamRepository.UpdateTeam(oldTeam);
                return 0; //no errors 
            }

            else return 1; //team not found 
        }


        public List<TeamOutDTO> GetAllTeams(int Page, int PageSize, bool? active, int? completedProjects, int? rating, int? hourlyRate)
        {
            var teams = _teamRepository.GetAllTeams();

            // Filters by if active if provided 
            if (active.HasValue)
            {
                teams = teams.Where(t => t.Active == active).ToList();
            }

            // Filters by completedProjects at if provided 
            if (completedProjects.HasValue)
            {
                teams = teams.Where(t => t.CompletedProjects >= completedProjects).ToList();
            }

            // Filters by rating at if provided 
            if (rating.HasValue)
            {
                teams = teams.Where(t => t.Rating >= rating).ToList();
            }

            // Filters by hourlyRate at if provided 
            if (hourlyRate.HasValue)
            {
                teams = teams.Where(t => t.HourlyRate <= hourlyRate).ToList();
            }


            //Mapping team to TeamOutDTO
            var OutTeams = new List<TeamOutDTO>();
            //Only outputing specific information to the user
            foreach (var team in teams)
            {
                var t = new TeamOutDTO
                {
                    TeamID = team.TeamID,
                    TeamName = team.TeamName,
                    Rating = team.Rating,
                    Description = team.Description,
                    TeamCapacity = team.TeamCapacity,
                    HourlyRate = team.HourlyRate,
                    CompletedProjects = team.CompletedProjects,
                    Active = team.Active,
                };
                OutTeams.Add(t);
            }

            // Paginating results and returning 
            int number = PageSize * Page;
            return OutTeams.OrderByDescending(t => t.TeamName).Skip(number).Take(PageSize).ToList();
        }
    }
}
