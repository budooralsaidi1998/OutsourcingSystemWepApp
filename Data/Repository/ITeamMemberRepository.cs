using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface ITeamMemberRepository
    {
        string AddTeamMember(TeamMember teamMember);
        bool CheckTMinTeam(int TeamID, int devID);
        void DeleteTeamMemebr(int teamID, int developerID);
        List<TeamMember> GetAllTeamMembers();
        int GetNoTakenSlots(int TeamID);
        TeamMember GetTeamByDeveloperID(int DevID);
        TeamMember GetTeamByTeamID(int TeamID);
        List<TeamMember> GetTeamMemberByTeamID(int TeamID);
        void UpdateTeamMember(TeamMember teamMember);
    }
}