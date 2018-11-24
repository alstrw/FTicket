using FTicket.Interfaces;
using FTicket.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTicket.ViewModels
{
    public class TicketViewModel : BindableBase
    {
        private readonly ITicketService ticketService;

        private DateTime createdTime;
        private string ticketKey;

        private string subject;
        private string holder;
        private string ticketStatus;

        public string Subject { get => subject; set => SetProperty(ref subject, value, () => SaveTicket.RaiseCanExecuteChanged()); }

        public string Holder { get => holder; set => SetProperty(ref holder, value, () => SaveTicket.RaiseCanExecuteChanged()); }

        public string TicketStatus { get => ticketStatus; set => SetProperty(ref ticketStatus, value, () => SaveTicket.RaiseCanExecuteChanged()); }

        public TicketViewModel(ITicketService service, Ticket ticket)
        {

            this.ticketService = service;
            subject = ticket.Subject;
            holder = ticket.Holder;
            ticketStatus = ticket.TicketStatus;

            createdTime = ticket.CreatedDateTime;
            ticketKey = ticket.TicketKey;

            SaveTicket = new DelegateCommand(OnSaveTicket, CanSaveTicket);
        }

        private bool CanSaveTicket()
        {
            return !string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(Holder);
        }

        private async void OnSaveTicket()
        {
            var ticket = new Ticket(ticketKey, createdTime, ticketStatus)
            {
                Holder = holder,
                Subject = subject
            };

            await ticketService.SaveTicketAsync(ticket);
        }

        public DelegateCommand SaveTicket { get; }
        

    }
}
