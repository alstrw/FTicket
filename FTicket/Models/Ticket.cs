using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTicket.Models
{
    public class Ticket
    {

        public Ticket(string ticketKey=null, DateTime? createdTime=null, string ticketStatus=null)
        {
            this.TicketKey = ticketKey ?? Guid.NewGuid().ToString();
            this.CreatedDateTime = createdTime ?? DateTime.UtcNow;
            this.TicketStatus = ticketStatus ?? "Open"; // Default open on new tickets.
        }

        public string TicketKey { get; private set; }

        public DateTime CreatedDateTime { get; private set; }

        public string Subject { get; set; }

        // What user/client does the ticket relate to?
        public string Holder { get; set; }

        public string TicketStatus { get; set; }

        public List<TicketMessage> TicketMessages { get; set; }
    }
}
