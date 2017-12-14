using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_API_Final.Models.Auto;

namespace Project_API_Final.Controllers
{
    [Produces("application/json")]
    [Route("api/Threads")]
    public class ThreadsController : Controller
    {
        private readonly DBForumContext _context;

        public ThreadsController(DBForumContext context)
        {
            _context = context;
        }

        // GET: api/Threads
        [HttpGet]
        public IEnumerable<Threads> GetThreads()
        {
            return _context.Threads;
        }

        // GET: api/Threads/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetThreads([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var threads = await _context.Threads.SingleOrDefaultAsync(m => m.ThreadId == id);

            if (threads == null)
            {
                return NotFound();
            }

            return Ok(threads);
        }

        // PUT: api/Threads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThreads([FromRoute] Guid id, [FromBody] Threads threads)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != threads.ThreadId)
            {
                return BadRequest();
            }

            _context.Entry(threads).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThreadsExists(id))
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

        // POST: api/Threads
        [HttpPost]
        public async Task<IActionResult> PostThreads([FromBody] Threads threads)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Threads.Add(threads);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ThreadsExists(threads.ThreadId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetThreads", new { id = threads.ThreadId }, threads);
        }

        // DELETE: api/Threads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThreads([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var threads = await _context.Threads.SingleOrDefaultAsync(m => m.ThreadId == id);
            if (threads == null)
            {
                return NotFound();
            }

            _context.Threads.Remove(threads);
            await _context.SaveChangesAsync();

            return Ok(threads);
        }

        private bool ThreadsExists(Guid id)
        {
            return _context.Threads.Any(e => e.ThreadId == id);
        }
    }
}