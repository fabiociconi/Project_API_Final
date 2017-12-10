using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_API_Final.Models;

namespace Project_API_Final.Controllers
{
	//XML e JSON
	[Produces("application/json", "application/xml")]
	[Route("api/Auths")]
    public class AuthsController : Controller
    {
        private readonly DBForumContext _context;

        public AuthsController(DBForumContext context)
        {
            _context = context;
        }

        // GET: api/Auths
        [HttpGet]
        public IEnumerable<Auth> GetAuth()
        {
            return _context.Auth;
        }

        // GET: api/Auths/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuth([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var auth = await _context.Auth.SingleOrDefaultAsync(m => m.Email == id);

            if (auth == null)
            {
                return NotFound();
            }

            return Ok(auth);
        }

        // PUT: api/Auths/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuth([FromRoute] string id, [FromBody] Auth auth)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != auth.Email)
            {
                return BadRequest();
            }

            _context.Entry(auth).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Auths
        [HttpPost]
        public async Task<IActionResult> PostAuth([FromBody] Auth auth)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Auth.Add(auth);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AuthExists(auth.Email))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAuth", new { id = auth.Email }, auth);
        }

        // DELETE: api/Auths/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuth([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var auth = await _context.Auth.SingleOrDefaultAsync(m => m.Email == id);
            if (auth == null)
            {
                return NotFound();
            }

            _context.Auth.Remove(auth);
            await _context.SaveChangesAsync();

            return Ok(auth);
        }

        private bool AuthExists(string id)
        {
            return _context.Auth.Any(e => e.Email == id);
        }
    }
}