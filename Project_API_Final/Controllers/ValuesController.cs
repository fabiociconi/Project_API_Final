using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Project_API_Final.Controllers
{


	/// /Fazendo testes para  ver saida XML e JSON
	[Produces("application/json", "application/xml")]

	[Route("api/[controller]")]
    public class ValuesController : Controller
    {
		//private readonly DBForumContext _context;

		//public ValuesController(DBForumContext context)
		//{
		//	_context = context;
		//	if (_context.Auth.Count() == 0)
		//	{
		//		_context.Auth.Add(new Auth{ Email = "Item1",Password="123" });
		//		_context.SaveChanges();
		//	}

		//}
		//// GET//
		[HttpGet("/api/items"), FormatFilter]
		public IEnumerable<string> GetAll()
		{
			return new string[] { "value1", "value2" };
			//return _context.Auth.ToList();		
		}
		
		// GET api/values
		[HttpGet]
        public IEnumerable<string> Get()
        {
			return new string[] { "value1", "value2" };
			//return _context.Auth.ToList();
		}

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
