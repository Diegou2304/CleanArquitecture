using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = options.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception($"El usuario con email {request.Email} no existe");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

           
            if (result.Succeeded)
            {
                var token = await GenerateToken(user);
                return new AuthResponse
                {
                    Id = user.Id,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Email = user.Email,
                    Username = user.UserName,
                };
                
               
            }

            throw new Exception();
          
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if(existingUser != null)
            {
                throw new Exception($"El username ya fue tomado por otra cuenta");
            }

            var existingEmaul = await _userManager.FindByEmailAsync(request.Email);
            
            if(existingEmaul != null)
            {
                throw new Exception("El Email ya fue tomado por otra cuenta");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                Name = request.Name,
                Lastnames = request.Lastnames,
                UserName = request.Username,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            
            if (!result.Succeeded)
            {
                throw new Exception($"El usuario no pudo ser creado: {result.Errors}");

            }

            await _userManager.AddToRoleAsync(user, "Operator");

            var token = await GenerateToken(user);
            return new RegistrationResponse
            {
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id,
                Username = user.UserName,
            };
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser applicationUser)
        {
            var userClaims = await _userManager.GetClaimsAsync(applicationUser);
            var roles = await _userManager.GetRolesAsync(applicationUser);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
                
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.UserName),
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                new Claim(CustomClaimTypes.Uid, applicationUser.Id)
            }.Union(userClaims).Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
                );

            return jwtSecurityToken;
        }
    }
}
