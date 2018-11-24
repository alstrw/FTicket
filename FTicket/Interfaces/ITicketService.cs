using FTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTicket.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetTicketsAsync();

        Ticket CreateTicket();

        Task<bool> SaveTicketAsync(Ticket ticket);
    }
}
