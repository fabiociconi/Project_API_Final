using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Project_API_Final.Models.Entity
{
    public class ReturnInfo<T>
    {
		public string Message { get; set; }
		public bool Error { get; set; }

		[IgnoreDataMember]
		private Dictionary<string, object> Properties { get; set; }


		public virtual TValue GetProperty<TValue>(string key)
		{

			if (Properties.TryGetValue(key, out object result))
			{
				return (TValue)result;
			}

			return default(TValue);
		}
		public virtual bool SetProperty<TValue>(string key, TValue value)
		{
			try
			{
				Properties[key] = value;
				return true;
			}
			catch
			{
				return false;
			}
		}

	}
}
