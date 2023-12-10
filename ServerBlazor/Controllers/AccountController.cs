using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerBlazor.Models.Entities;
using ServerBlazor.Models;
using ServerBlazor.Data;

namespace ServerBlazor.Controllers
{
    // Kode hentet og modifisert fra Modul 7

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAccountRepository _repo;

        public AccountController(UserManager<IdentityUser> userManager, ApplicationDbContext appDbContext, IAccountRepository repo)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _repo = repo;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("verifyLogin")]
        // POST: api/Account/verifyLogin
        public async Task<IActionResult> VerifyLogin(BlogUser user)
        {
            var res = await _repo.VerifyCredentials(user);

            if (res == null)
            {
                return new StatusCodeResult(406);
            }

            return new ObjectResult(_repo.GenerateJwtToken(res));
        }

        [HttpGet]
        [Route("logout")]
        //[Authorize]
        // GET: api/Account/logout
        public async Task<IActionResult> Logout()
        {
            await _repo.LogoutUser();
            //if (returnUrl != null)
            //{
            //}

            return Ok();
        }
        
    }



    }
