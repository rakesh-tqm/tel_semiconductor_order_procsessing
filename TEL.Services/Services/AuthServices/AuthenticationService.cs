using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace TEL.Services
{
    public static class AuthenticationService
    {
        public static string JwtKey;
        public static string JwtIssuer;

        static AuthenticationService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            JwtKey = configuration.GetValue<string>("Jwt:Key") ?? string.Empty;
            JwtIssuer = configuration.GetValue<string>("Jwt:Issuer") ?? string.Empty;

        }
        /// <summary>
        /// Authenticate Login
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public static string AuthenticateLogin(LoginViewModel loginInfo)
        {
            try
            {
                var isAuthenticate = loginInfo.Email == "admin" && loginInfo.Password == "123";

                if (isAuthenticate)
                {
                    return GenerateJSONWebToken(loginInfo);

                }
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Generate JWT Token
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static string GenerateJSONWebToken(LoginViewModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
                        new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
               };

            var token = new JwtSecurityToken(JwtIssuer,
                JwtIssuer,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
