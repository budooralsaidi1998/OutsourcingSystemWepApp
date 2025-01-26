using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface IDeveloperSkillRepository
    {
        string AddDeveloperSkill(DeveloperSkill developerSkill);
        bool CheckDevHasSkill(int devID, int skillID);
        void DeleteDeveloperSkill(int DevID, int SkillID);
        List<DeveloperSkill> GetAllDeveloperSkills();
        List<DeveloperSkill> GetAllSkillsForDev(int devID);
        List<DeveloperSkill> GetDevelopersBySkillID(int SkillID);
        DeveloperSkill GetDeveloperSkillByIDs(int devID, int skillID);
        List<DeveloperSkill> GetSkillsByDeveloperID(int DevID);
        void UpdateDeveloperSkills(List<DeveloperSkill> skills);
    }
}