using OutsourcingSystemWepApp.Data.DTOs;
using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;

namespace OutsourcingSystemWepApp.Services
{
    public class RequestService : IRequestService
    {

        private readonly IClientRequestDeveloperRepository _developerRequestRepository;
        private readonly IClientRequestTeamRepository _teamRequestRepository;
        private readonly IEmailService _emailService;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepositry _userRepository;
        private readonly IUserServices _userServices;
        private readonly IDeveloperRepositry _developerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IProjectServieces _projectServieces;

        public RequestService(
            IClientRequestDeveloperRepository developerRequestRepository,
            IClientRequestTeamRepository teamRequestRepository,
            IEmailService emailService,
            IClientRepository clientRepository,
            IUserServices userServices,
            IUserRepositry userRepository,
            IDeveloperRepositry developerRepository,
            ITeamRepository teamRepository,
             IProjectServieces projectServieces)
        {
            _developerRequestRepository = developerRequestRepository;
            _teamRequestRepository = teamRequestRepository;
            _emailService = emailService;
            _clientRepository = clientRepository;
            _userServices = userServices;
            _userRepository = userRepository;
            _developerRepository = developerRepository;
            _teamRepository = teamRepository;
            _projectServieces = projectServieces;
        }

        public async Task SubmitRequestAsync(int userid, ProjectRequestInputDto project)
        {
            if (userid == 0)
            {
                throw new InvalidOperationException("ClientID cannot be null.");
            }

            int clientId = _clientRepository.GetByuid(userid).ClientID;
            string email = _userServices.GetEmail(userid);

            if (project.RequestType == "Developer")
            {
                var developer = _developerRepository.GetById(project.Developerid ?? 0);
                if (developer == null || !developer.AvailabilityStatus)
                {
                    throw new InvalidOperationException("The selected developer is not available for booking.");
                }

                var developerRequest = new ClientRequestDeveloper
                {
                    ClientID = clientId,
                    DeveloperID = project.Developerid ?? 0,
                    StartDate = project.StartAt,
                    EndDate = project.EndAt,
                    Status = "Pending"
                };


                await _developerRequestRepository.AddRequestAsync(developerRequest);

            }
            else if (project.RequestType == "Team")
            {
                var team = _teamRepository.GetTeamByID(project.Teamid ?? 0);
                if (team == null || !team.IsAvailable)
                {
                    throw new InvalidOperationException("The selected team is not available for booking.");
                }

                var teamRequest = new ClientRequestTeam
                {
                    ClientID = clientId,
                    TID = project.Teamid ?? 0,
                    StartDate = project.StartAt,
                    EndDate = project.EndAt,
                    Status = "Pending"
                };
                await _teamRequestRepository.AddRequestAsync(teamRequest);
                //   _projectServieces.AddProject(clientId, project);
            }

            string clientEmailMessage =
            $"Dear Client,\n\n" +
            $"Your request has been submitted successfully. Below are the details of your request:\n\n" +
            $"- Request Type: {project.RequestType}\n" +
            $"- Start Date: {project.StartAt.ToShortDateString()}\n" +
            $"- End Date: {project.EndAt.Value.ToShortDateString()}\n" +
            "\nWe will notify you once your request is processed. Thank you for choosing our service.";

            await _emailService.SendEmailAsync(email, "New Request Submitted", clientEmailMessage);


            string adminEmail = "amanialshmali7@gmail.com";

            string adminEmailMessage =
            $"Dear Admin,\n\n" +
           $"A new request has been submitted. Below are the details:\n\n" +
           $"- Request Type: {project.RequestType}\n" +
           $"- Client ID: {clientId}\n" +
           $"- Start Date: {project.StartAt.ToShortDateString()}\n" +
           $"- End Date: {project.EndAt.Value.ToShortDateString()}\n" +
           "\nPlease review the request and take the necessary actions.";

            await _emailService.SendEmailAsync(adminEmail, "New Request Submitted", adminEmailMessage);

            _projectServieces.AddProject(clientId, project);
        }

        public async Task ProcessRequestAsync(int requestId, bool isAccepted, string requestType)
        {
            string clientEmail = "";

            if (requestType == "Developer")
            {
                var request = await _developerRequestRepository.GetRequestByIdAsync(requestId);
                if (request == null)
                {
                    throw new InvalidOperationException("Developer request not found.");
                }

                var developer = _developerRepository.GetById(request.DeveloperID);
                if (developer == null)
                {
                    throw new InvalidOperationException("The developer is not found.");
                }

                request.Status = isAccepted ? "Approved" : "Rejected";
                await _developerRequestRepository.UpdateRequestAsync(request);

                if (isAccepted)
                {
                    developer.AvailabilityStatus = false;
                    _developerRepository.Update(developer);
                }

                var client = _clientRepository.GetById(request.ClientID);
                var user = _userRepository.GetUserById(client.UID);
                clientEmail = user?.Email ?? throw new InvalidOperationException("Client email not found.");
            }
            else if (requestType == "Team")
            {
                var request = await _teamRequestRepository.GetRequestByIdAsync(requestId);
                if (request == null)
                {
                    throw new InvalidOperationException("Team request not found.");
                }

                var team = _teamRepository.GetTeamByID(request.TID);
                if (team == null)
                {
                    throw new InvalidOperationException("The team is not found.");
                }

                request.Status = isAccepted ? "Approved" : "Rejected";
                await _teamRequestRepository.UpdateRequestAsync(request);

                if (isAccepted)
                {
                    team.IsAvailable = false;
                    _teamRepository.UpdateTeam(team);
                }

                var client = _clientRepository.GetById(request.ClientID);
                var user = _userRepository.GetUserById(client.UID);
                clientEmail = user?.Email ?? throw new InvalidOperationException("Client email not found.");
            }

            string clientStatusMessage =
            isAccepted
              ? $"Dear Client,\n\nYour request of type '{requestType}' has been approved successfully. Below are the details:\n\n" +
              $"- Request Type: {requestType}\n" +
              $"- Request ID: {requestId}\n" +
              $"- Status: Approved\n\n" +
              "Thank you for choosing our service. If you have any questions, please contact support."
              : $"Dear Client,\n\nWe regret to inform you that your request of type '{requestType}' has been rejected. Below are the details:\n\n" +
              $"- Request Type: {requestType}\n" +
              $"- Request ID: {requestId}\n" +
              $"- Status: Rejected\n\n" +
              "We apologize for any inconvenience caused. Please contact support for more details.";

            await _emailService.SendEmailAsync(clientEmail, "Request Status Update", clientStatusMessage);

        }

        public async Task<IEnumerable<PendingRequestDto>> GetPendingRequestsAsync()
        {
            var developerRequests = await _developerRequestRepository.GetPendingRequestsAsync();

            var teamRequests = await _teamRequestRepository.GetPendingRequestsAsync();


            developerRequests = developerRequests ?? new List<ClientRequestDeveloper>();
            teamRequests = teamRequests ?? new List<ClientRequestTeam>();

            var pendingDeveloperRequests = developerRequests
                .Where(req => req.Status == "Pending")
                .Select(req => new PendingRequestDto
                {
                    RequestId = req.RequestID,
                    Type = "Developer",
                    ClientName = req.Client.CompanyName,
                    StartDate = req.StartDate,
                    EndDate = req.EndDate,
                    Status = req.Status
                });

            var pendingTeamRequests = teamRequests
                .Where(req => req.Status == "Pending")
                .Select(req => new PendingRequestDto
                {
                    RequestId = req.RequestID,
                    Type = "Team",
                    ClientName=req.Client.CompanyName,
                    ClientID = req.ClientID,
                    StartDate = req.StartDate,
                    EndDate = req.EndDate ?? DateTime.MaxValue,
                    Status = req.Status
                });

            return pendingDeveloperRequests.Concat(pendingTeamRequests);
        }




        //    public async Task<IEnumerable<PendingRequestDto>> GetAcceptAndReject()
        //    {
        //        var developerAccept = await _developerRequestRepository.ApprovedRequest();
        //        var developerReject = await _developerRequestRepository.RejectedRequest();


        //        var teamAccept = await _teamRequestRepository.ApprovedRequest();

        //        var teamReject = await _teamRequestRepository.RejectedRequest();


        //        developerAccept = developerAccept ?? new List<ClientRequestDeveloper>();
        //        developerReject= developerReject ?? new List<ClientRequestDeveloper>();

        //        teamAccept = teamAccept ?? new List<ClientRequestTeam>();
        //        teamReject= teamReject ?? new List<ClientRequestTeam>();
        //        var AcceptDeveloperRequests = developerAccept
        //            .Where(req => req.Status == "Approved")
        //            .Select(req => new PendingRequestDto
        //            {
        //                RequestId = req.RequestID,
        //                Type = "Developer",
        //                ClientName = req.Client.CompanyName,
        //                StartDate = req.StartDate,
        //                EndDate = req.EndDate,
        //                Status = req.Status
        //            });
        //        var RejectDeveloperRequests = developerReject
        //       .Where(req => req.Status == "Rejected")
        //       .Select(req => new PendingRequestDto
        //       {
        //           RequestId = req.RequestID,
        //           Type = "Developer",
        //           ClientName = req.Client.CompanyName,
        //           StartDate = req.StartDate,
        //           EndDate = req.EndDate,
        //           Status = req.Status
        //       });
        //        var AcceptTeamRequests = teamAccept
        //            .Where(req => req.Status == "Approved")
        //            .Select(req => new PendingRequestDto
        //            {
        //                RequestId = req.RequestID,
        //                Type = "Team",
        //                ClientID = req.ClientID,
        //                StartDate = req.StartDate,
        //                EndDate = req.EndDate ?? DateTime.MaxValue,
        //                Status = req.Status
        //            });
        //        var RejectTeamRequests = teamReject
        //         .Where(req => req.Status == "Rejected")
        //         .Select(req => new PendingRequestDto
        //         {
        //             RequestId = req.RequestID,
        //             Type = "Team",
        //             ClientID = req.ClientID,
        //             StartDate = req.StartDate,
        //             EndDate = req.EndDate ?? DateTime.MaxValue,
        //             Status = req.Status
        //         });

        //        return AcceptDeveloperRequests
        //  .Concat(RejectDeveloperRequests)
        //  .Concat(AcceptTeamRequests)
        //  .Concat(RejectTeamRequests);
        //    }

        //}
    }
}

