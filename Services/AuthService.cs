using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using OutsourcingSystemWepApp.Data.Model;
using OutsourcingSystemWepApp.Data.Repository;
using OutsourcingSystemWepApp.helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OutsourcingSystemWepApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IUserRepositry _userRepo;

        public AuthService(JwtSettings jwtSettings, IJSRuntime jsRuntime, AuthenticationStateProvider authenticationStateProvider, IUserRepositry userRepo)
        {
            _jwtSettings = jwtSettings;
            _jsRuntime = jsRuntime;
            _authenticationStateProvider = authenticationStateProvider;
            _userRepo = userRepo;
        }



        public async Task<string> Login(string email, string password)
        {
            var user = _userRepo.Login(email, password);
            if (user == null) return null;

            var tokenResponse = GenerateToken(user);
            await SaveTokenToLocalStorage(tokenResponse.Token);
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(tokenResponse.Token);

            return tokenResponse.Token;
        }

        public JwtTokenResponse GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UID.ToString()),
                new Claim(ClaimTypes.Role, user.role),

            };


            if (user.role == "Client")
            {
                var clientID = _userRepo.GetClientIDByUID(user.UID);
                if (clientID != null)
                {
                    claims.Add(new Claim("ClientID", clientID.ToString()));
                }
            }

            if (user.role == "Developer")
            {
                var developerID = _userRepo.GetDeveloperIDByUID(user.UID);
                if (developerID != null)
                {
                    claims.Add(new Claim("DeveloperID", developerID.ToString()));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signingCredentials: creds
            );

            return new JwtTokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes)
            };
        }

        private async Task SaveTokenToLocalStorage(string token)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);
        }
    }
}
