using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SMECService.ViewModels;
using System.Threading.Tasks;

namespace SMECService.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        #region Constructor
        public RoleController(RoleManager<IdentityRole> roleManager,
            ILogger<RoleController> logger,
            IConfiguration configuration)
        {
            _roleManager = roleManager;
            _logger = logger;
            _configuration = configuration;
        }
        #endregion

     


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]RoleRequestViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);
            if (await _roleManager.RoleExistsAsync(model.name))
                return new StatusCodeResult(403);
            var result = await _roleManager.CreateAsync(new IdentityRole(model.name));
            if (result.Succeeded)

                return new StatusCodeResult(201);
            else 
                return new StatusCodeResult(500);
        }


        #region Properties
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        #endregion
    }
}
