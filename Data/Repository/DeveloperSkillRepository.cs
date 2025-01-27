using Microsoft.EntityFrameworkCore;
using OutsourcingSystemWepApp.Data.DTOs;
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

        //new 

        public void RemoveDeveloperSkill(int developerID, int skillID)
        {
            var skill = _context.DeveloperSkill.FirstOrDefault(d => d.DeveloperID == developerID && d.SkillID == skillID);

            if (skill != null)
            {
                _context.DeveloperSkill.Remove(skill);
                _context.SaveChanges();
            }
        }


        public List<DeveloperSkill> GetSkillsByDeveloperId(int developerID)
        {
            return _context.DeveloperSkill.Where(ds => ds.DeveloperID == developerID).ToList();
        }






        //new to fex erro


        public void AddDeveloperSkilll(DeveloperSkill developerSkill)
        {
            var existingSkill = _context.DeveloperSkill
                .FirstOrDefault(ds => ds.DeveloperID == developerSkill.DeveloperID && ds.SkillID == developerSkill.SkillID);

            if (existingSkill == null)
            {
                _context.DeveloperSkill.Add(developerSkill);
                _context.SaveChanges();
            }
        }



        public void UpdateDeveloperSkill(DeveloperSkill developerSkill)
        {
            var existingSkill = _context.DeveloperSkill
                .FirstOrDefault(ds => ds.DeveloperID == developerSkill.DeveloperID && ds.SkillID == developerSkill.SkillID);

            if (existingSkill != null)
            {
                existingSkill.Proficiency = developerSkill.Proficiency;
                _context.SaveChanges();
            }
        }

        public void DeleteDeveloperSkilll(int developerID, int skillID)
        {
            var skill = _context.DeveloperSkill
                .FirstOrDefault(d => d.DeveloperID == developerID && d.SkillID == skillID);

            if (skill != null)
            {
                _context.DeveloperSkill.Remove(skill);
                _context.SaveChanges();
            }
        }



        public List<DeveloperSkillDTO> GetDeveloperSkills(int developerID)
        {
            return _context.DeveloperSkill
                .Where(ds => ds.DeveloperID == developerID)
                .Select(ds => new
                {
                    ds.DeveloperID,
                    ds.SkillID,
                    Skill = _context.Skills.FirstOrDefault(s => s.SkillID == ds.SkillID)
                })
                .AsEnumerable()
                .Select(ds => new DeveloperSkillDTO
                {
                    DeveloperID = ds.DeveloperID,
                    SkillID = ds.SkillID,
                    SkillName = ds.Skill != null ? ds.Skill.Name : "Unknown Skill",
                    Proficiency = ds.SkillID
                })
                .ToList();
        }


        public string GetSkillName(int skillID)
        {
            return _context.Skills.FirstOrDefault(s => s.SkillID == skillID)?.Name ?? "Unknown Skill";
        }


    }
}
