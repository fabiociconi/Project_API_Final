using Microsoft.EntityFrameworkCore;
using Project_API_Final.Models.Auto;
using Project_API_Final.Models.Entity;
using System.Linq;

namespace Project_API_Final.Models.BusinessRules
{
	public class AuthBusiness
    {
		public TokenEntity Signin(string email, string password)
		{
			Auth auth = GetAuthByUser(email);

			if (auth.Password == password)
			{
				UserBusiness userBusiness = new UserBusiness();
				Users user = userBusiness.GetUserById(auth.UserId);
				TokenEntity token = new TokenEntity
				{
					User = user
				};
				return token;

			}
			return null;

		}

		public Auth GetAuthByUser(string email)
		{
			using (var context = new DBForumContext(new DbContextOptions<DBForumContext>()))
			{
				
				var auth = context.Auth
					.Where(b => b.Email == email)
					.FirstOrDefault();
				return auth;
			}
			
		}

	}
}
