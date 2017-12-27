using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project_API_Final.Models.Auto;
using Project_API_Final.Models.BusinessRules;
using Project_API_Final.Models.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_API_Final.Controllers
{
	[Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : BaseController
	{


		[HttpPost]
		public TokenEntity SignInAsync([FromBody] string user, string password)
		{
			AuthBusiness auth = new AuthBusiness();
			var result = auth.Signin(user, password);

			if (result != null)
			{
				TokenEntity token = result;
				token.Token = new JwtSecurityTokenHandler().WriteToken(GetTokenAsync(token));
				return token;
			}
			return null;

		}
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

	}
}
