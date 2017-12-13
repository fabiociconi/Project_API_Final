using Project_API_Final.Enum;
using Project_API_Final.Models.Auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Project_API_Final.Models.Entity
{
    public class TokenEntity
    {

		public string Token { get; set; }
		public Users User { get; set; }
		[IgnoreDataMember]
		public RoleType RoleType { get; set; }
	}
}
