using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public interface IDeveloperRepositry
    {
        void Add(Developer developer);
        bool Delete(Developer deve);
        IEnumerable<Developer> GetAll();
        IEnumerable<Developer> Getavailibilty(bool availibilty);
        Developer GetById(int id);
        IEnumerable<Developer> GetNameDeveloper(string name);
        IEnumerable<Developer> getrate(decimal rate);
        IEnumerable<Developer> GetSpec(string spe);
        Developer GetUserById(int userId);
        void Update(Developer dev);
    }
}