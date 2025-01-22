using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Services
{
    public interface ITeamService
    {
        int AddTeam(int adminID, TeamInDTO input);
        bool CheckTeamByID(int TeamID);
        int DeleteTeam(int TeamID);
        List<Team> GetTeamsBasedOnSearchValue(string value);
        List<TeamOutDTO> GetAllTeams(int Page, int PageSize, bool? active, int? completedProjects, int? rating, int? hourlyRate);
        Team GetTeamByID(int TeamID);
        int ReactivateTeam(int TeamID);
        int UpdateTeam(int TeamID, int AdminID, TeamUpdateDTO team);
    }
}