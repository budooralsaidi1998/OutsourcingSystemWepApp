using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;

namespace OutsourcingSystemWepApp.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        public SkillService(ISkillRepository skillrepo)
        {
            _skillRepository = skillrepo;
        }

        //Adds skill using input from user 
        public int AddSkill(string name, string description)
        {
            //mapping skillInDTO to skill 
            var skill = new Skill
            {
                Name = name,
                Description = description,
                CreatedAt = DateTime.Now,
                Active = true,
            };

            return _skillRepository.AddSkill(skill);
        }

        //Soft deleting skill
        public int DeleteSkill(int SkillID)
        {
            try
            {
                var skill = _skillRepository.GetSkillByID(SkillID);

                //Checking if the skill exists
                if (skill != null)
                {
                    skill.Active = false; //deactivating skill

                    _skillRepository.UpdateSkill(skill);

                    return 0; //if everything went well then 1 will be returned 
                }

                else return 1; //skill not found
            }
            catch { return 2; } //an error occured when trying to save
        }

        //Reactivating after soft delete
        public int ReactivateSkill(int SkillID)
        {
            try
            {
                var skill = _skillRepository.GetSkillByID(SkillID);

                //Checking if the skill exists
                if (skill != null)
                {
                    skill.Active = true; //reactivating skill

                    _skillRepository.UpdateSkill(skill);

                    return 0; //if everything went well then 1 will be returned 
                }

                else return 1; //skill not found
            }
            catch { return 2; } //an error occured when trying to save
        }


        //Adds skill using input from user 
        public int UpdateSkill(int skillID, string name, string description)
        {
            var oldskill = _skillRepository.GetSkillByID(skillID);

            if (oldskill != null)
            {
                //mapping skillInDTO to skill 
                oldskill.Name = name;
                oldskill.Description = description;

                _skillRepository.UpdateSkill(oldskill);
                return 0; //no errors 
            }

            else return 1; //skill not found 
        }

        public List<Skill> GetAllSkills(int Page, int PageSize, bool? active, DateTime? createdAt)
        {
            var skills = _skillRepository.GetAllSkills();

            // Filters by if active if provided 
            if (active.HasValue)
            {
                skills = skills.Where(s => s.Active == active).ToList();
            }

            // Filters by created at if provided 
            if (createdAt.HasValue)
            {
                string dateOnly = createdAt?.ToString("yyyy-MM-dd"); //removing the time so that only the dates are being compared
                skills = skills.Where(s => s.CreatedAt.ToString("yyyy-MM-dd") //also removing the time from the actual date added to ensure that they are equal
                == dateOnly).ToList();
            }

            // Paginating results and returning 
            int number = PageSize * Page;
            return skills.OrderByDescending(p => p.Name).Skip(number).Take(PageSize).ToList();
        }
    }
}
