using Microsoft.EntityFrameworkCore;
using Project_API_Final.Models.Auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_API_Final.Models.BusinessRules
{
    public class RegisterBusiness
    {
		//validar se ja exite depois... para nao deixar incluir mais de um//

		public bool Signup(Users user)
		{
			using (var context = new DBForumContext(new DbContextOptions<DBForumContext>()))
			{

				Auth au = new Auth
				{
				
					Email = user.GetInfoAuth().Email,
					Password = user.GetInfoAuth().Password
				};

				Users rr = new Users
				{
					UserId = new Guid(),
					FirstName = user.FirstName,
					LastName = user.LastName,
					DateCreated = DateTime.Now
				};
				context.Auth.Add(au);
				context.Users.Add(rr);
				context.SaveChanges();

				return true;
				
			}			
		}
	}
}
