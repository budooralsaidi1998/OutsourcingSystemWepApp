using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly ApplictionDbContext _context;

        public SkillRepository(ApplictionDbContext context)
        {
            _context = context;
        }

        //Adds a new skill [returns skill id]
        public int AddSkill(Skill skill)
        {
            _context.Skills.Add(skill);
            _context.SaveChanges();
            return skill.SkillID;
        }

        //Update skill details also used for soft delete [does not return anything]
        public void UpdateSkill(Skill skill)
        {
            _context.Skills.Update(skill);
            _context.SaveChanges();
        }

        //Hard deleting skill
        public void DeleteSkill(Skill skill)
        {
            _context.Skills.Remove(skill);
        }

        //Gets all skills [returns list of skills]
        public List<Skill> GetAllSkills()
        {
            return _context.Skills.ToList();
        }

        //Get by ID [returns skill by ID]
        public Skill GetSkillByID(int ID)
        {
            return _context.Skills.FirstOrDefault(s => s.SkillID == ID);
        }
    }
}
