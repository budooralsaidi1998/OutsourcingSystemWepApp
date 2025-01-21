using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface ITeamRepository
    {
        int AddTeam(Team team);
        void DeleteTeam(Team team);
        List<Team> GetAllTeams();
        Team GetTeamByID(int ID);
        void UpdateTeam(Team team);
    }
}