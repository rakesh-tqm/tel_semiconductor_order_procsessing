using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelGws.API.Infrastructures;

namespace TelGws.API.Controllers.Account
{
    public class AccountController : TelBaseController
    {
        /// <summary>
        /// Get Login Info
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("/login")]
        [HttpPost]
        public IActionResult Login([FromForm] LoginViewModel loginInfo)
        {
            var userToken = AuthenticationService.AuthenticateLogin(loginInfo);

            if (userToken != null)
            {
                return Ok(GetAjaxResponse(true, string.Empty, new { token = userToken }));
            }

            return Unauthorized();
        }

        /// <summary>
        /// Get Access Token
        /// </summary>
        /// <returns></returns>
        [HttpGet("/getAccessToken")]
        public IActionResult GetAccessToken()
        {
            IActionResult response = Unauthorized();

            string userName = HttpContext.User?.Identity?.Name;
            if (userName != null)
            {
                string sanitizedUser = userName.Contains(@"\") ? userName.Substring(userName.IndexOf(@"\") + 1) : userName;

                var token = AuthenticationService.GenerateJSONWebToken(new LoginViewModel { Email = sanitizedUser });

                if (token != null)
                {
                    response = Ok(new { token });
                }
            }
            return response;
        }

        [HttpGet("/public-page")]
        [AllowAnonymous]
        public IActionResult AllowAnonymous()
        {
            return Ok(new string[] { "value1", "value2", "value3", "value4", "value5" });
        }


        [HttpGet("/auth-required")]
        public IActionResult AuthRequired()
        {
            return Ok(new string[] { "value1", "value2", "value3", "value4", "value5" });
        }



    }
}
