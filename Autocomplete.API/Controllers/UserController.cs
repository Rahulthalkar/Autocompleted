using ANM.Domain;
using Autocomplete.BLL;
using Autocomplete.Domain;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace Autocomplete.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Route("Login")]
        [HttpPost]
        [ProducesResponseType(typeof(APIResult<LoginInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResult<LoginInfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login(LoginModel model)
        {
            log.Info($"Request: {nameof(Login)}  method called with value {JsonConvert.SerializeObject(model)}.");
            if (model.Username != null && model.Password != null)
            {
                var responseOfLogin = _userService.Login(model);
                if (responseOfLogin!=null)
                {
                    log.Info($"Response: {nameof(Login)} method {JsonConvert.SerializeObject(responseOfLogin)}");
                    return Ok(responseOfLogin);
                }
                else
                {
                    log.Info($"Response: {nameof(Login)} method {JsonConvert.SerializeObject(responseOfLogin)}");
                    return BadRequest(responseOfLogin);
                }
            }
            else
            {
                return BadRequest("Login Failed");
            }

        }

    }
}
