using FTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTicket.Services
{
    public class TestTicketService
    {
        public IEnumerable<Ticket> GetTickets()
        {
            yield return new Ticket() { Subject = "Test subject", Holder = "Alastair Wilkins" };
            yield return new Ticket() { Subject = "A different one", Holder = "Joe Bloggs" };
            yield return new Ticket() { Subject = "More tickets", Holder = "Alastair Wilkins" };
            yield return new Ticket() { Subject = "One  more ticket", Holder = "Alastair Wilkins" };
            yield return new Ticket() { Subject = "ok just one more", Holder = "Jane Doe" };
        }
    }
}
