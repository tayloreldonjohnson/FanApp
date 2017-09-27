using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hello.Data;
using Hello.Data.Models;
using Microsoft.EntityFrameworkCore.Internal;
using MoreLinq;
using Hello.Migrations;

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
        public IEnumerable<InboxUserVm> GetInbox()
        {
            return _context.Inbox;
        }
        public class UserInboxVm
        {
            public string MessagerUserId { get; set; }
            public string UserName { get; set; }
            public string profileImage { get; set; }
            public DateTime DateCreated { get; set; }
            public string message { get; set; }
      
        }

        // -----------------------------------------------Still Testing------------------------------------------------------------------------------------------------
        //[HttpGet("messageandprofile/{recieverofMessageid}")]
        ////public List<Post> GetFollowedPost(string id)
        //public List<Inbox> GetInboxtWithProfile(string recieverofMessageid)
        //{
        //    var inboxes = new UserInboxVm();
        //    var MessagesFromOneUser = new List<UserInboxVm>();
        //   // get a list of all messages intended for user 
        //    var messages = _context.Inbox.Where(um => um.RecieverOfMessageId == recieverofMessageid).OrderByDescending(m => m.DateCreated).OrderBy(um => um.MessagerUserId).ToList();

        //    // loop through the list and get info about the message
        //    foreach (var message in messages)
        //    {
        //        var sender = _context.ApplicationUser.Where(u => u.Id == message.MessagerUserId).FirstOrDefault();




        //        inboxes.message = message.Message;
        //        inboxes.DateCreated = message.DateCreated;


        //    }

        //    //MessagesFromOneUser.Add(inboxes);


        //    return messages;

        //}
        //----------------------------------------------------------------------Still Testing --------------------------------------------------------------------------------------------------------------
        public class SendersOfMessageVm
        {
            public string UserName { get; set; }
            public string ProfileImage { get; set; }
            public  DateTime LatestMessageDate { get; set; }
            public string Snippet { get; set; }
            public string senderId { get; set; }
        }

        [HttpGet("messageandprofile/{recieverofMessageid}")]
        //public List<Post> GetFollowedPost(string id)
        public List<SendersOfMessageVm> GetInboxtWithProfile(string recieverofMessageid)
        {
          
            
            var ListOfSenders = new List<SendersOfMessageVm>();
            // get a list of all messages intended for user 
            var userMessages = _context.Inbox.Where(um => um.RecieverOfMessageId == recieverofMessageid).ToList();
     
            // loop through the list and get info about the message
            foreach (var message in userMessages)
            {
                var user = _context.ApplicationUser.Where(u => u.Id == message.MessagerUserId).FirstOrDefault();
                var messagevm = _context.Inbox.Where(si => si.MessagerUserId == user.Id).Where(ri => ri.RecieverOfMessageId == recieverofMessageid)
                         .OrderByDescending(d => d.DateCreated).FirstOrDefault();
                var snippet = "";
                if (messagevm.Message.Length < 5  )
                {

                    snippet = messagevm.Message;
                }
                if (messagevm.Message == null || messagevm.Message == "")
                {
                    snippet = "No message";
                }
                else
                {
                    snippet = messagevm.Message.Substring(0, 1) + "...";
                }
                var sender = new SendersOfMessageVm()
                {

                    UserName = user.UserName,
                    ProfileImage = user.ImageUrl,
                    LatestMessageDate = messagevm.DateCreated,
                    Snippet = snippet, 
                    senderId = user.Id

                };

                ListOfSenders.Add(sender);

            }
            return ListOfSenders.DistinctBy(u => u.UserName).ToList();

        }


        //---------------------------------------


        [HttpGet("message/{senderid}/{recieverId}")]
        //public List<Post> GetFollowedPost(string id)
        public List<UserInboxVm> GetMessageOfUser(string senderId, string recieverId)
        {
            var Messages = _context.Inbox.Where(x => x.MessagerUserId == senderId && x.RecieverOfMessageId == recieverId && x.MessagerUserId == senderId && x.RecieverOfMessageId == recieverId).ToList();
            var ListOfMessages = new List<UserInboxVm>();

            foreach (var Message in Messages)
            {

                var user = _context.ApplicationUser.Where(u => u.Id == Message.MessagerUserId).FirstOrDefault();
                var Messagevm = new UserInboxVm
                {
                    message = Message.Message,
                    UserName = user.UserName,
                    profileImage = user.ImageUrl,
                    DateCreated = Message.DateCreated
                };
                ListOfMessages.Add(Messagevm);
            }
            return ListOfMessages;
        }

		//public class LogUserInboxVm
		//{
		//	public string MessagerUserId { get; set; }
		//	public string LogUserName { get; set; }
		//	public string UserprofileImage { get; set; }
		//	public DateTime UserDateCreated { get; set; }
		//	public string usermessage { get; set; }

		//}

		//[HttpGet("usermessage/{recieverId}/{senderid}")]
		////public List<Post> GetFollowedPost(string id)
		//public List<UserInboxVm> GetMessagefromUser(string senderId, string recieverId)
		//{
		//	var Messages = _context.Inbox.Where(x => x.MessagerUserId == senderId && x.RecieverOfMessageId == recieverId).ToList();
		//	var ListOfMessages = new List<UserInboxVm>();

		//	foreach (var Message in Messages)
		//	{

		//		var user = _context.ApplicationUser.Where(u => u.Id == Message.MessagerUserId).FirstOrDefault();
		//		var UserMessagevm = new LogUserInboxVm
		//		{
		//			usermessage = Message.Message,
		//			LogUserName = user.UserName,
		//			UserprofileImage = user.ImageUrl,
		//			UserDateCreated = Message.DateCreated


		//		};
		//		ListOfMessages.Add(LogUserInboxVm);
		//	}
		//	return ListOfMessages;
		//}



		//--------------------------------------------------------------------------------------------------------Testing get first message of all users----------------
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
        public async Task<IActionResult> PutInbox([FromRoute] int id, [FromBody] InboxUserVm inbox)
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
        public async Task<IActionResult> PostInbox([FromBody] InboxUserVm inbox)
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