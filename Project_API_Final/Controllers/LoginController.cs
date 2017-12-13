using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project_API_Final.Models.BusinessRules;
using Project_API_Final.Models.Entity;

namespace Project_API_Final.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : BaseController
	{

		private JwtSecurityToken GetTokenAsync(TokenEntity token)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, token.User.UserId.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, $"{token.User.FirstName} {token.User.LastName}"),
				new Claim(ClaimTypes.Role, token.RoleType.ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConstants.TokenKey));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


			return new JwtSecurityToken(AppConstants.TokenAudience, AppConstants.TokenAudience, claims, expires: DateTime.Now.AddDays(30), signingCredentials: credentials);
		}

		[HttpPost]
		public TokenEntity SignIn([FromBody] string user, string password)
		{
			AuthBusiness auth = new AuthBusiness();
			var result = auth.Signin(user, password);

			if (result != null)
			{
				TokenEntity tt = result;
				tt.Token = new JwtSecurityTokenHandler().WriteToken(GetTokenAsync(tt));
				return tt;
			}
			return null;

		}
	}
}
