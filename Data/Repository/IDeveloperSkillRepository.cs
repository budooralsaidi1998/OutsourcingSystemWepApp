using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface IDeveloperSkillRepository
    {
        string AddDeveloperSkill(DeveloperSkill developerSkill);
        void AddDeveloperSkilll(DeveloperSkill developerSkill);
        bool CheckDevHasSkill(int devID, int skillID);
        void DeleteDeveloperSkill(int DevID, int SkillID);
        void DeleteDeveloperSkilll(int developerID, int skillID);
        List<DeveloperSkill> GetAllDeveloperSkills();
        List<DeveloperSkill> GetAllSkillsForDev(int devID);
        List<DeveloperSkill> GetDevelopersBySkillID(int SkillID);
        DeveloperSkill GetDeveloperSkillByIDs(int devID, int skillID);
        List<DeveloperSkillDTO> GetDeveloperSkills(int developerID);
        string GetSkillName(int skillID);
        List<DeveloperSkill> GetSkillsByDeveloperId(int developerID);
        List<DeveloperSkill> GetSkillsByDeveloperID(int DevID);
        void RemoveDeveloperSkill(int developerID, int skillID);
        void UpdateDeveloperSkill(DeveloperSkill developerSkill);
        void UpdateDeveloperSkills(List<DeveloperSkill> skills);
    }
}