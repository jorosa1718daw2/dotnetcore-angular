using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SMECService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SMECService.ViewModels;

namespace SMECService.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        #region Constructor
        public UserController(UserManager<ApplicationUser> userManager,
            ILogger<UserController> logger,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }
        #endregion

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]UserCreateViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);


            if (await _userManager.FindByNameAsync(model.name) == null)
            {
                var result = await _userManager.CreateAsync(new ApplicationUser
                {
                    SecurityStamp = System.Guid.NewGuid().ToString(),
                    UserName = model.name,
                    Email = model.email,

                }, model.password);

                if (result.Succeeded)
                    return new StatusCodeResult(201);
                else
                    return new StatusCodeResult(500);
            }
            return new StatusCodeResult(403);
        }

        #region Properties
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        #endregion
    }
}
