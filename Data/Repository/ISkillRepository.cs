using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface ISkillRepository
    {
        int AddSkill(Skill skill);
        void DeleteSkill(Skill skill);
        List<Skill> GetAllSkills();
        List<(Skill skill, int DeveloperCount)> GetAllSkillsWithDeveloperCount();
        Skill GetSkillByID(int ID);
        int GetSkillByName(string name);
        void UpdateSkill(Skill skill);
    }
}