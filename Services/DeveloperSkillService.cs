using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;

namespace OutsourcingSystemWepApp.Services
{
    public class DeveloperSkillService : IDeveloperSkillService
    {
        private readonly IDeveloperSkillRepository _DeveloperSkillRepository;
        public DeveloperSkillService(IDeveloperSkillRepository developerSkillRepository)
        {
            _DeveloperSkillRepository = developerSkillRepository;
        }

        //Adds developer skill using input from user 
        public string AddDeveloperSkill(int skillID, int developerID, int proficiency = 0)
        {
            //Check that developer skill exists
            var devSkillExists = _DeveloperSkillRepository.GetDeveloperSkillByIDs(developerID, skillID);

            if (devSkillExists == null)
            {
                //mapping input to developerSkill 
                var devSkill = new DeveloperSkill
                {
                    DeveloperID = developerID,
                    SkillID = skillID,
                    Proficiency = proficiency
                };

                return _DeveloperSkillRepository.AddDeveloperSkill(devSkill);
            }

            else return "<!>This developer skill relationship already exists<!>";
        }

        //Deleting developer skill [returns 0 no errors or 1 error occured]
        public int DeleteDeveloperSkill(int skillID, int DeveloperID)
        {
            try
            {
                //Check that developer skill relation exists 
                var devSkillExists = _DeveloperSkillRepository.GetDeveloperSkillByIDs(DeveloperID, skillID);

                if (devSkillExists != null)
                {
                    _DeveloperSkillRepository.DeleteDeveloperSkill(DeveloperID, skillID);
                    return 0; //no errors
                }
                else return 1; //developer skill relationship does not exist
            }
            catch { return 2; } //error occured 
        }

        public List<DeveloperSkill> GetAllDeveloperSkills(int Page, int PageSize, int? developerID, int? skillID)
        {
            var devSkills = _DeveloperSkillRepository.GetAllDeveloperSkills();

            // Filters by if developerID if provided 
            if (developerID.HasValue)
            {
                devSkills = devSkills.Where(t => t.DeveloperID == developerID).ToList();
            }

            // Filters by skillID at if provided 
            if (skillID.HasValue)
            {
                devSkills = devSkills.Where(t => t.SkillID == skillID).ToList();
            }

            // Paginating results and returning 
            int number = PageSize * Page;
            return devSkills.OrderBy(t => t.DeveloperID).Skip(number).Take(PageSize).ToList();
        }

        public List<DeveloperSkill> GetSkillByDevID(int DevID)
        {
            //validate output -> skill does not exist, no developers found
            //Check that developer skill relation exists 
            var devSkillExists = _DeveloperSkillRepository.GetAllSkillsForDev(DevID);

            if (devSkillExists != null)
            {
                return _DeveloperSkillRepository.GetAllSkillsForDev(DevID);
            }
            else return null; // ("<!>This developer does not have skills or does not exist<!>");

        }

        //Checks id a dev has a specific skill [returns true / false] 
        public bool CheckDevHasSkill(int devID, int skillID)
        {
            var skillFound = _DeveloperSkillRepository.CheckDevHasSkill(devID, skillID);
            return skillFound == true ? true : false;
        }

        public List<DeveloperSkill> GetDevelopersBySkill(int skillID)
        {
            return _DeveloperSkillRepository.GetDevelopersBySkillID(skillID);
        }

        public async Task UpdateDeveloperSkills(int developerId, List<DeveloperSkillDTO> skills)
        {

            var existingSkills = _DeveloperSkillRepository.GetSkillsByDeveloperID(developerId);

            foreach (var skillDto in skills)
            {
                var existingSkill = existingSkills.FirstOrDefault(s => s.SkillID == skillDto.SkillID);

                if (existingSkill != null)
                {

                    existingSkill.Proficiency = skillDto.Proficiency;
                }
                else
                {
                    var newSkill = new DeveloperSkill
                    {
                        DeveloperID = developerId,
                        SkillID = skillDto.SkillID,
                        Proficiency = skillDto.Proficiency
                    };

                    _DeveloperSkillRepository.AddDeveloperSkill(newSkill);
                }
            }

            await Task.CompletedTask;
        }
    }
}
