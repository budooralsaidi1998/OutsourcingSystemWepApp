using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Services
{
    public interface IDeveloperSkillService
    {
        string AddDeveloperSkill(int skillID, int developerID, int proficiency = 0);
        DeveloperSkillDTO AddDeveloperSkilll(int skillID, int developerID, int proficiency);
        bool CheckDevHasSkill(int devID, int skillID);
        int DeleteDeveloperSkill(int skillID, int DeveloperID);
        List<DeveloperSkill> GetAllDeveloperSkills(int Page, int PageSize, int? developerID, int? skillID);
        List<DeveloperSkill> GetDevelopersBySkill(int skillID);
        List<DeveloperSkill> GetDeveloperSkills(int developerID);
        List<DeveloperSkill> GetSkillByDevID(int DevID);
        void RemoveDeveloperSkill(int developerID, int skillID);
        void UpdateDeveloperSkill(int developerID, int skillID, int proficiency);
        Task UpdateDeveloperSkills(int developerId, List<DeveloperSkillDTO> skills);
    }
}