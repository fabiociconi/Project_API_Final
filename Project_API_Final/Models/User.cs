using Project_API_Final.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_API_Final.Models
{
	public class User
	{


		public int ID { get; set; }
		public string LastName { get; set; }
		public string FirstMidName { get; set; }
		public UserType Type { get; set; }




	}
}

