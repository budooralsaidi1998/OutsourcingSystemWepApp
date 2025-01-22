using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Services
{
    public interface ISkillService
    {
        int AddSkill(string name, string description);
        int DeleteSkill(int SkillID);
        List<Skill> GetAllSkills(int Page, int PageSize, bool? active, DateTime? createdAt);
        int ReactivateSkill(int SkillID);
        int UpdateSkill(int skillID, string name, string description);
        List<(Skill skill, int DeveloperCount)> GetSkillsWithDeveloperCount();
    }
}