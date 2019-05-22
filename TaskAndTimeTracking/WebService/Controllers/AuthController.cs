using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskAndTimeTracking.Controller.DTO.request;
using TaskAndTimeTracking.Controller.DTO.response;
using TaskAndTimeTracking.Controller.interfaces;

namespace TaskAndTimeTracking.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthenticationController AuthenticationController;
        private IUserController UserController;

        public AuthController(IAuthenticationController authenticationController, IUserController userController)
        {
            AuthenticationController = authenticationController;
            UserController = userController;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<ActionResult<ResponseDTO<TokenDTO>>> getToken([FromBody] CredentialsDTO credentials)
        {
            if (credentials.UserName == null || credentials.Password == null)
            {
                return new ResponseDTO<TokenDTO>("No credentials were given");
            }

            if (!await AuthenticationController.CredentialsValid(credentials))
            {
                return new ResponseDTO<TokenDTO>("Credentials invalid");
            }
            return new ResponseDTO<TokenDTO>(await AuthenticationController.GenerateToken(credentials.UserName));
        }
        
    }
}