using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_API_Final.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Project_API_Final.Controllers
{
	[Produces("application/json")]
	[Route("api/Base")]
	public abstract class BaseController : Controller
	{
		public bool IsAuthenticated
		{
			get
			{
				return HttpContext.User.Identities.Any(c => c.IsAuthenticated);
			}
		}
		public Guid UserKey
		{
			get
			{
				var result = Guid.Empty;

				if (!IsAuthenticated)
				{
					return result;
				}
				var identifier = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

				if (identifier.Equals(string.Empty))
				{
					return result;
				}

				Guid.TryParse(identifier, out result);

				return result;
			}
		}

		public UserConst UserInfo
		{
			get
			{
				var userKey = UserKey;

				if (userKey == Guid.Empty)
				{
					return null;
				}

				return new UserConst
				{
					Key = userKey,
					Name = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value
				};
			}
		}

	}

}