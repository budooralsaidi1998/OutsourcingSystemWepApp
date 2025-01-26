using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using OutsourcingSystemWepApp.Components;
using OutsourcingSystemWepApp.Configurations;
using OutsourcingSystemWepApp.Data.Repository;
using OutsourcingSystemWepApp.helpers;
using OutsourcingSystemWepApp.Services;

namespace OutsourcingSystemWepApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Add services to the container.
            builder.Services.AddDbContext<ApplictionDbContext>(

                 options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

                  );


           // builder.Services.AddScoped<IUserRepositry, UserRepositry>();
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();
            builder.Services.AddScoped<ITeamRepository, TeamRepository>();
            builder.Services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IDeveloperSkillRepository, DeveloperSkillRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IReviewTeamRepository, ReviewTeamRepository>();
            builder.Services.AddScoped<IReviewDevRepository, ReviewDevRepository>();
            builder.Services.AddScoped<IFeedBackOnClientRepository, FeedBackOnClientRepository>();
            builder.Services.AddScoped<IDeveloperRepositry, DeveloperRepositry>();
            builder.Services.AddScoped<IProjectServieces, ProjectServieces>();
            builder.Services.AddScoped<IProjectRepositry, ProjectRepositry>();
            builder.Services.AddScoped<IFeedBackOnClientService, FeedBackOnClientService>();
            builder.Services.AddScoped<IReviewTeamService, ReviewTeamService>();
            builder.Services.AddScoped<IReviewDeveloperService, ReviewDeveloperService>();
            builder.Services.AddScoped<IDeveloperSkillService, DeveloperSkillService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddScoped<ISkillService, SkillService>();
           // builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<ITeamMemberService, TeamMemberService>();
            builder.Services.AddScoped<IJointService, JointService>();
            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddScoped<ISkillService, SkillService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IDeveloperServices, DeveloperServices>();

            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<IClientRequestDeveloperRepository, ClientRequestDeveloperRepository>();
            builder.Services.AddScoped<IClientRequestTeamRepository, ClientRequestTeamRepository>();

            // Register EmailSettings and EmailService
             builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            builder.Services.AddScoped<IEmailService, EmailService>();



            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IUserRepositry, UserRepositry>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<IAdminService, AdminService>();


            builder.Services.AddMudServices();
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
