using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hello.Data;
using Hello.Data.Models;

namespace Hello.Controllers
{
    [Produces("application/json")]
    [Route("api/Inboxes")]
    public class InboxesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InboxesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Inboxes
        [HttpGet]
        public IEnumerable<Inbox> GetInbox()
        {
            return _context.Inbox;
        }

        // GET: api/Inboxes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInbox([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inbox = await _context.Inbox.SingleOrDefaultAsync(m => m.Id == id);

            if (inbox == null)
            {
                return NotFound();
            }

            return Ok(inbox);
        }

        // PUT: api/Inboxes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInbox([FromRoute] int id, [FromBody] Inbox inbox)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inbox.Id)
            {
                return BadRequest();
            }

            _context.Entry(inbox).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InboxExists(id))
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

        // POST: api/Inboxes
        [HttpPost]
        public async Task<IActionResult> PostInbox([FromBody] Inbox inbox)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Inbox.Add(inbox);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInbox", new { id = inbox.Id }, inbox);
        }

        // DELETE: api/Inboxes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInbox([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inbox = await _context.Inbox.SingleOrDefaultAsync(m => m.Id == id);
            if (inbox == null)
            {
                return NotFound();
            }

            _context.Inbox.Remove(inbox);
            await _context.SaveChangesAsync();

            return Ok(inbox);
        }

        private bool InboxExists(int id)
        {
            return _context.Inbox.Any(e => e.Id == id);
        }
    }
}