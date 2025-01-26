using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public class DeveloperSkillRepository : IDeveloperSkillRepository
    {
        private readonly ApplictionDbContext _context;

        public DeveloperSkillRepository(ApplictionDbContext context)
        {
            _context = context;
        }

        //Adds a new DeveloperSkill [returns DeveloperSkill id]
        public string AddDeveloperSkill(DeveloperSkill developerSkill)
        {
            _context.DeveloperSkill.Add(developerSkill);
            _context.SaveChanges();
            return ($"Skill " + developerSkill.SkillID + " added to developer " + developerSkill.DeveloperID);
        }

        //No update because only two values > it would be like adding a new one so just delete and add

        //Hard deleting DeveloperSkill
        public void DeleteDeveloperSkill(int DevID, int SkillID)
        {
            var skill = _context.DeveloperSkill.FirstOrDefault(d => d.DeveloperID == DevID && d.SkillID == SkillID);
            _context.DeveloperSkill.Remove(skill);
            _context.SaveChanges();
        }

        //Gets all DeveloperSkill [returns list of developerSkill]
        public List<DeveloperSkill> GetAllDeveloperSkills()
        {
            return _context.DeveloperSkill.ToList();
        }

        //Get by Developer ID [returns skills for specific developer]
        public List<DeveloperSkill> GetSkillsByDeveloperID(int DevID)
        {
            return _context.DeveloperSkill.Where(d => d.DeveloperID == DevID).ToList();
        }

        //Get by Skill ID [returns Developerskill]
        public List<DeveloperSkill> GetDevelopersBySkillID(int SkillID)
        {
            return _context.DeveloperSkill.Where(d => d.SkillID == SkillID).ToList();
        }

        //checks that this developer has this skill  
        public bool CheckDevHasSkill(int devID, int skillID)
        {
            return _context.DeveloperSkill.Any(d => d.DeveloperID == devID && d.SkillID == skillID);
        }

        //Gets all team skills by developerID [returns list of developerskills]
        public List<DeveloperSkill> GetAllSkillsForDev(int devID)
        {
            return _context.DeveloperSkill.Where(d => d.DeveloperID == devID).ToList();
        }

        //Gets Developer skill by skillID and DevID [returns developerSkill]
        public DeveloperSkill GetDeveloperSkillByIDs(int devID, int skillID)
        {
            return _context.DeveloperSkill.FirstOrDefault(d => d.DeveloperID == devID && d.SkillID == skillID);
        }

        public void UpdateDeveloperSkills(List<DeveloperSkill> skills)
        {
            foreach (var skill in skills)
            {
                var existingSkill = _context.DeveloperSkill
                    .FirstOrDefault(s => s.DeveloperID == skill.DeveloperID && s.SkillID == skill.SkillID);

                if (existingSkill != null)
                {
                    existingSkill.Proficiency = skill.Proficiency;
                }
                else
                {
                    _context.DeveloperSkill.Add(skill);
                }
            }

            _context.SaveChanges();
        }
    }
}
