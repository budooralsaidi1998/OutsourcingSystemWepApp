using Microsoft.EntityFrameworkCore;
using OutsourcingSystemWepApp.Data.Model;

namespace OutsourcingSystemWepApp.Data.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplictionDbContext _context;


        public ClientRepository(ApplictionDbContext context)
        {
            _context = context;
        }

        // get all clients that are not marked as deleted
        public IEnumerable<Client> GetAll()
        {
            try
            {
                return _context.Client.Where(c => !c.IsDeleted).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("An error happen while retrieving all clients.", ex);
            }
        }

        // get  client by ID, ensuring it is not marked as deleted
        public Client GetById(int id)
        {
            try
            {
                return _context.Client.FirstOrDefault(c => c.ClientID == id && !c.IsDeleted);
            }
            catch (Exception ex)
            {

                throw new Exception($"An error happen while retrieving the client with ID {id}.", ex);
            }
        }



        public Client GetByuid(int id)
        {
            try
            {
                return _context.Client.FirstOrDefault(c => c.UID == id && !c.IsDeleted);
            }
            catch (Exception ex)
            {

                throw new Exception($"An error happen while retrieving the client with ID {id}.", ex);
            }
        }
        // get clients by a specific industry, ensuring they are not marked as deleted
        public IEnumerable<Client> GetByIndustry(string industry)
        {
            try
            {
                return _context.Client.Where(c => c.Industry == industry && !c.IsDeleted).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception($"An error happen while retrieving clients in the industry: {industry}.", ex);
            }
        }

        // get clients with a rating greater than or equal to the specified value
        public IEnumerable<Client> GetByRating(decimal rating)
        {
            try
            {
                return _context.Client.Where(c => c.CommitmentRating >= rating && !c.IsDeleted).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception($"An error happen while retrieving clients with a rating of {rating} or higher.", ex);
            }
        }

        //public IEnumerable<Client> GetUnapprovedClients()
        //{
        //    return _context.Client.Where(c => !c.IsApprove && !c.IsDeleted).ToList();
        //}
        // Adds a new client to the database and saves changes
        public void Add(Client client)
        {
            try
            {
                _context.Client.Add(client);
                _context.SaveChanges(); // Saves changes  after adding
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database Update Exception: {dbEx.Message}");
                Console.WriteLine($"Inner exception: {dbEx.InnerException?.Message}");
                throw new Exception("Database error while adding a new client.", dbEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
                throw new Exception("An error occurred while adding a new client.", ex);
            }
        }


        // Updates  client in the database and saves change

        public void Update(Client client)
        {
            try
            {
                _context.Client.Update(client);

                _context.SaveChanges(); // Saves change after updating
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while updating the client.", ex);
            }
        }

        // Marks a client as deleted (soft delete) 
        public void SoftDelete(Client client)
        {
            try
            {
                client.IsDeleted = true; // Mark the client as deleted

                _context.Client.Update(client);

                _context.SaveChanges(); // Saves changes updating the deletion status
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while soft deleting the client.", ex);
            }
        }

    }
}
