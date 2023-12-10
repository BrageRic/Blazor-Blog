using ServerBlazor.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ServerBlazor.Data;

namespace ServerBlazor.Models
{
    // Kode Hentet og modifisert fra Modul 7

    /// <summary>
    ///     Repository for handling interactions with database pertaining BlogUser
    /// </summary>
    /// <see cref="BlogUser"/>
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _conf;

        public AccountRepository(SignInManager<IdentityUser> manager, UserManager<IdentityUser> userManager, ApplicationDbContext db,  IConfiguration conf)
        {
            _db = db;
            _conf = conf;
            _signInManager = manager;
            _userManager = userManager;
        }

        /// <summary>
        ///     Verifies user login
        /// </summary>
        /// <see cref="BlogUser"/>
        /// <param name="user">User object to be verified</param>
        /// <returns>User object with a jwt bearer token</returns>
        public async Task<BlogUser> VerifyCredentials(BlogUser user)
        {
            if (user.Username == null || user.Password == null || user.Username.Length == 0 || user.Password.Length == 0)
                return null;

            var thisUser = await _userManager.FindByNameAsync(user.Username);
            if (thisUser == null)
                return null;

            var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, false, lockoutOnFailure: true);
            if (!result.Succeeded)
                return null;
            
            return new BlogUser() { Username = user.Username};
        }

        /// <summary>
        ///     Generates a token for a user
        /// </summary>
        /// <param name="user">User token will be generated for</param>
        /// <returns>Jwt token string</returns>
        public string GenerateJwtToken(BlogUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var confKey = _conf.GetSection("TokenSettings")["SecretKey"];
            var key = Encoding.ASCII.GetBytes(confKey);
            var cIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = cIdentity,
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public Task<bool> ChangePasswd(BlogUser u, string oldP, string newP)
        {
            throw new NotImplementedException();
        }

        public Task<BlogUser> ChangeRole(BlogUser u, string newR)
        {
            throw new NotImplementedException();
        }
        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public Task<bool> DeleteUser(BlogUser u)
        {
            throw new NotImplementedException();
        }

        public Task<BlogUser> AddUser(BlogUser u)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlogUser>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
