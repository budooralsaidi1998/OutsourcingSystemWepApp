using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;

namespace OutsourcingSystemWepApp.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplictionDbContext _context;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepositry _userRepositry;

        public ClientService(ApplictionDbContext context, IClientRepository clientRepository, IUserRepositry userRepositry)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userRepositry = userRepositry;
            _clientRepository = clientRepository;


        }

        //public void RegisterClient(UserInputDto client)
        //{
        //    // Validate the input DTO to ensure it is not null

        //    if (client == null)
        //        throw new ArgumentNullException(nameof(client), "Client data cannot be null.");

        //    // Check that the CompanyName is provided and not  null

        //    if (string.IsNullOrEmpty(client.CompanyName))

        //        throw new ArgumentException("Company name is required.");

        //    var existingUserByEmail = _userRepositry.GetUserByEmail(client.Email);
        //    if (existingUserByEmail != null)
        //    {
        //        throw new ArgumentException("A user with this email already exists.");
        //    }

        //    // Check for duplicate password
        //    var existingUserByPassword = _userRepositry.GetUserByPassword(client.Password);
        //    if (existingUserByPassword != null)
        //    {
        //        throw new ArgumentException("A user with this password already exists. Please choose a different password.");
        //    }

        //    // Validate CompanyName
        //    if (string.IsNullOrWhiteSpace(client.CompanyName))
        //    {
        //        throw new ArgumentException("Company name is required.");
        //    }
        //    if (client.CompanyName.Length > 100)
        //    {
        //        throw new ArgumentException("Company name cannot be more than 100 characters.");
        //    }

        //    // Validate Industry
        //    if (!string.IsNullOrEmpty(client.Industry) && client.Industry.Length > 100)
        //    {
        //        throw new ArgumentException("Industry name cannot be more than 100 characters.");
        //    }


        //    // Validate Notes
        //    if (!string.IsNullOrEmpty(client.Notes) && client.Notes.Length > 1000)
        //    {
        //        throw new ArgumentException("Notes cannot be more than 1000 characters.");
        //    }

        //    try
        //    {
        //        // Map the data from ClientDTO to a new Client 

        //        client.Password = BCrypt.Net.BCrypt.HashPassword(client.Password);
        //        var user = new User
        //        {
        //            Name = client.Name,
        //            Email = client.Email,
        //            Password = client.Password,
        //            role = client.role,
        //            CreatedAt = client.CreatedAtClient,
        //        };
        //        // Add the new client entity to the repository
        //        int userID = _userRepositry.AddUserInt(user);

        //        var Clienet = new Client
        //        {
        //            UID = userID,
        //            CompanyName = client.CompanyName, // Assign company name 
        //            Industry = client.Industry,       // Assign industry 
        //            Notes = client.Notes,
        //            CreatedAt = DateTime.Now             //  date is current time
        //        };
        //        _clientRepository.Add(Clienet);



        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("An error occurred while registering the client.", ex);
        //    }
        //}


        public async Task<bool> RegisterClient(RegisterClientDto clientDto)
        {
            if (string.IsNullOrWhiteSpace(clientDto.Industry) || string.IsNullOrWhiteSpace(clientDto.CompanyName))
            {
                throw new ArgumentException("Industry and Company Name are required fields.");
            }

            // Check if the email already exists
            var existingUser = await _userRepositry.GetUserByEmailAsync(clientDto.Email.ToLower());
            if (existingUser != null)
            {
                throw new ArgumentException("This email is already registered.");
            }

            // Create a new user
            var user = new User
            {
                Name = clientDto.Name,
                Email = clientDto.Email.ToLower(),
                Password = BCrypt.Net.BCrypt.HashPassword(clientDto.Password), // Hash password
                role = "Client",
                CreatedAt = DateTime.UtcNow
            };

            // Add user to database
            int userId = await _userRepositry.AddUserIntAsync(user);

            // Create a new client associated with the user
            var client = new Client
            {
                UID = userId,
                CompanyName = clientDto.CompanyName,

                Industry = clientDto.Industry,
                Notes = string.Empty,
                PhoneNumber = clientDto.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            _context.Client.Add(client);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Database Error: {ex.InnerException?.Message}");
            }

            return true;
        }



        public void UpdateClient(int id, UpdateClientData updatedClientDto)
        {
            try
            {
                // Ensure the provided updated client data is not null

                if (updatedClientDto == null)

                    throw new ArgumentNullException(nameof(updatedClientDto), "Updated client data cannot be null.");

                // get the existing client from the repository using  ID
                var client = _clientRepository.GetByuid(id);

                if (client == null)

                    throw new KeyNotFoundException($"Client with ID {id} not found.");

                if (client.IsDeleted == true)
                {
                    throw new Exception("Cannot update a deleted account. Please log out.");
                }

                // Update client fields only if new values are provided, otherwise retain existing values
                client.CompanyName = updatedClientDto.CompanyName ?? client.CompanyName;
                client.Industry = updatedClientDto.Industry ?? client.Industry;
                client.UpdateDate = DateTime.Now;
                // Update the client in the repository
                _clientRepository.Update(client);


            }
            catch (ArgumentNullException ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            catch (KeyNotFoundException ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }
        //public void ApproveClient(ApprovalDto approval)
        //{
        //    var client = _clientRepository.GetById(approval.ClientId);
        //    if (client == null)
        //    {
        //        throw new ArgumentException("Client not found.");
        //    }

        //    client.IsApprove = approval.IsApprove;
        //  //  client.ApprovedByAdmin = approval.ApprovedByAdmin; // Optional: Add ApprovedByAdmin in the Client class.
        //    _clientRepository.Update(client);
        //}
        public void SoftDeleteClient(int id)
        {
            // Validate the client ID to ensure >1 
            if (id <= 0)
                throw new ArgumentException("Client ID must be greater than zero.");

            try
            {
                // get the client by ID from the repository
                var client = _clientRepository.GetByuid(id);

                // Check if the client exists
                if (client == null)
                    throw new KeyNotFoundException($"Client with ID {id} not found.");

                // Mark the client as soft-deleted in the repository
                _clientRepository.SoftDelete(client);


            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while soft-deleting the client with ID {id}.", ex);
            }
        }

        public IEnumerable<ClientDTO> GetAllClients(string name, string industry, decimal? rating, int pageNumber, int pageSize)
        {
            // Validate pagination parameters to ensure they are positive integers

            if (pageNumber <= 0 || pageSize <= 0)
                throw new ArgumentException("Page number and page size must be greater than zero.");

            try
            {
                // get all clients from the repository and convert to a query object

                var query = _clientRepository.GetAll().AsQueryable();

                // Filter by name if a name is provided
                if (!string.IsNullOrEmpty(name))
                    query = query.Where(c => c.CompanyName.Contains(name, StringComparison.OrdinalIgnoreCase));

                // Filter by industry if an industry is provided
                if (!string.IsNullOrEmpty(industry))
                    query = query.Where(c => c.Industry.Contains(industry));

                // Filter by rating if a rating is provided
                if (rating.HasValue)
                    query = query.Where(c => c.CommitmentRating >= rating);

                // Apply pagination
                return query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(c => new ClientDTO        // Map the Client entity to ClientDTO
                    {
                        ClientID = c.ClientID,
                        userid = c.UID,
                        CompanyName = c.CompanyName,
                        Industry = c.Industry,
                        Rating = c.CommitmentRating,
                        CreatedAt = c.CreatedAt
                    })
                    .ToList(); // Execute the query and return the results as a list
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while retrieving clients.", ex);
            }
        }


        public IEnumerable<ClientByIndestry> GetClientsByIndustry(string industry)
        {
            // Validate the industry parameter to ensure it not null 

            if (string.IsNullOrEmpty(industry))
                throw new ArgumentException("Industry cannot be null or empty.");

            try
            {
                // get clients by industry from the repository

                // Map the result to a collection of ClientDTO 
                return _clientRepository.GetByIndustry(industry).Select(c => new ClientByIndestry
                {
                    ClientID = c.ClientID,
                    CompanyName = c.CompanyName,
                    Industry = industry,
                    Rating = c.CommitmentRating,
                    CreatedAt = c.CreatedAt

                    // Assign the creation date
                });
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while retrieving clients in the industry '{industry}'.", ex);
            }
        }

        public ClientDTO GetClientById(int id)
        {
            try
            {
                // Validate that the ID is a positive number
                if (id <= 0)
                    throw new ArgumentException("The client ID must be greater than 0.", nameof(id));

                // Retrieve the client from the repository
                var client = _clientRepository.GetByuid(id);
                if (client == null)
                    throw new KeyNotFoundException($"Client with ID {id} not found.");

                // Map the client entity to a ClientDTO and return it
                return new ClientDTO
                {
                    ClientID = client.ClientID,
                    userid = id,
                    CompanyName = client.CompanyName,
                    Industry = client.Industry,
                    Rating = client.CommitmentRating,
                    CreatedAt = client.CreatedAt
                };
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"Validation Error: {ex.Message}");
                throw;
            }
            catch (KeyNotFoundException ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }



        public IEnumerable<ClientByIndestry> GetClientsByRating(decimal rating)
        {
            // Validate input Rating must not be negative.

            if (rating < 0)
                throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be >= 0.");

            try
            {
                // get clients from the repository with a rating >= the specified value.

                return _clientRepository.GetByRating(rating).Select(c => new ClientByIndestry
                {
                    ClientID = c.ClientID,              // Map ClientID
                    CompanyName = c.CompanyName,        // Map Company Name
                    Industry = c.Industry,              // Map Industry
                    Rating = c.CommitmentRating,        // Map Rating
                    CreatedAt = c.CreatedAt             // Map CreatedAt timestamp
                });
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while retrieving clients with a rating >= {rating}.", ex);
            }
        }

        //new 


        public updateClientDtocs GetClientProfile(int clientId)
        {
            var client = _clientRepository.GetClientProfile(clientId);
            return new updateClientDtocs
            {
                ClientID = client.ClientID,
                CompanyName = client.CompanyName,
                Industry = client.Industry
            };
        }

        public List<string> GetPreviousProjects(int clientId)
        {
            return _clientRepository.GetPreviousProjects(clientId);
        }

        public List<string> GetCurrentProjects(int clientId)
        {
            return _clientRepository.GetCurrentProjects(clientId);
        }

        public void UpdateIndustry(int clientId, string industry)
        {
            _clientRepository.UpdateIndustry(clientId, industry);
        }


    }
}
