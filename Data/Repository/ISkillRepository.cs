using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface ISkillRepository
    {
        int AddSkill(Skill skill);
        void DeleteSkill(Skill skill);
        List<Skill> GetAllSkills();
        Skill GetSkillByID(int ID);
        void UpdateSkill(Skill skill);
        List<(Skill skill, int DeveloperCount)> GetAllSkillsWithDeveloperCount();
    }
}