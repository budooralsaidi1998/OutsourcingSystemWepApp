using Microsoft.EntityFrameworkCore;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public class DeveloperRepositry : IDeveloperRepositry
    {
        private readonly ApplictionDbContext _context;


        public DeveloperRepositry(ApplictionDbContext context)
        {
            _context = context;
        }

        public Developer GetById(int id)
        {
            try
            {
                return _context.Developer.FirstOrDefault(c => c.DeveloperID == id && !c.IsDelete);
            }
            catch (Exception ex)
            {

                throw new Exception($"An error happen while retrieving the Developer with ID {id}.", ex);
            }
        }
        public void Add(Developer developer)
        {
            try
            {
                _context.Developer.Add(developer);
                _context.SaveChanges(); // Saves changes  after adding
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database Update Exception: {dbEx.Message}");
                Console.WriteLine($"Inner exception: {dbEx.InnerException?.Message}");
                throw new Exception("Database error while adding a new developer.", dbEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
                throw new Exception("An error occurred while adding a new client.", ex);
            }
        }

        public void Update(Developer dev)
        {
            try
            {
                _context.Developer.Update(dev);

                _context.SaveChanges(); // Saves change after updating
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while updating the Developer.", ex);
            }
        }

        public IEnumerable<Developer> GetAll()
        {
            return _context.Developer.ToList();
        }

        // Marks a client as deleted (soft delete) 
        public bool Delete(Developer deve)
        {

            _context.Developer.Remove(deve);
            _context.SaveChanges();
            return true;
        }
        public Developer GetUserById(int userId)
        {
            return _context.Developer
                .Where(u => u.UserID == userId)
                .FirstOrDefault();
        }
        public IEnumerable<Developer> GetNameDeveloper(string name)
        {
            try
            {
                return _context.Developer.Where(c => c.DeveloperName == name && !c.IsDelete).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception($"An error happen while retrieving developer in the name: {name}.", ex);
            }
        }

        public IEnumerable<Developer> GetSpec(string spe)
        {
            try
            {
                return _context.Developer.Where(c => c.Specialization == spe && !c.IsDelete).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception($"An error happen while retrieving developer in the spec: {spe}.", ex);
            }
        }

        public IEnumerable<Developer> Getavailibilty(bool availibilty)
        {
            try
            {
                return _context.Developer.Where(c => c.AvailabilityStatus == availibilty && !c.IsDelete).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception($"An error happen while retrieving developer in the availabilty: {availibilty}.", ex);
            }
        }
        public IEnumerable<Developer> getrate(decimal rate)
        {
            try
            {
                return _context.Developer.Where(c => c.CommitmentRating == rate && !c.IsDelete).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception($"An error happen while retrieving developer in the hourrate: {rate}.", ex);
            }
        }

    }
}
