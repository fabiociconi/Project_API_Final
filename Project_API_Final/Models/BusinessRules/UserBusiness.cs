using Microsoft.EntityFrameworkCore;
using Project_API_Final.Models.Auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_API_Final.Models.BusinessRules
{
    public class UserBusiness
    {
		public Users GetUserById(Guid userId)
		{
			using (var context = new DBForumContext(new DbContextOptions<DBForumContext>()))
			{

				var user = context.Users.Find(userId);

				return user;
			}
		}
    }
}
