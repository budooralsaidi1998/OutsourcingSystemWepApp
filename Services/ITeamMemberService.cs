using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Services
{
    public interface ITeamMemberService
    {
        string AddTeamMember(int teamID, int developerID);
        bool CheckTMinTeam(int TeamID, int devID);
        int DeleteTeamMember(int TeamID, int DeveloperID);
        List<TeamMember> GetAllTeamMembers(int Page, int PageSize, int? developerID, int? teamID);
        int GetNoTakenSlots(int TeamID);
        List<TeamMember> GetTeamMemberByTeamID(int TeamID);
        int IsDevInTeam(int devID);
        int UpdateTeamMember(int teamID, int developerID);
    }
}