using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Services
{
    public interface IDeveloperSkillService
    {
        string AddDeveloperSkill(int skillID, int developerID, int proficiency = 0);
        bool CheckDevHasSkill(int devID, int skillID);
        int DeleteDeveloperSkill(int skillID, int DeveloperID);
        List<DeveloperSkill> GetAllDeveloperSkills(int Page, int PageSize, int? developerID, int? skillID);
        List<DeveloperSkill> GetDevelopersBySkill(int skillID);
        List<DeveloperSkill> GetSkillByDevID(int DevID);
        Task UpdateDeveloperSkills(int developerId, List<DeveloperSkillDTO> skills);
    }
}