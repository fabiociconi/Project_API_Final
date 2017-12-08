using Project_API_Final.Enum;
using Project_API_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_API_Final.Data
{
    public static class DBInitializer
    {
		public static void Initialize(ForumContext context)
		{
			context.Database.EnsureCreated();
			if (context.Users.Any())
			{
				return;   // DB has been seeded
			}
			var users = new User[]
			{
			new User{FirstMidName="Fabio",LastName="Ciconi",Type=UserType.admin},
			new User{FirstMidName="Rodrigo",LastName="Geronimo",Type=UserType.client}
			};
			
			foreach (User s in users)
			{
				context.Users.Add(s);
			}
			context.SaveChanges();

		}

	}
}
