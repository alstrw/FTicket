using FTicket.Interfaces;
using FTicket.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTicket.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly ITicketService _service;
        private TicketViewModel selectedTicket;

        public MainViewModel(ITicketService service)
        {
            _service = service;

            this.CreateTicket = new DelegateCommand(OnCreateTicket);
        }

        private void OnCreateTicket()
        {
            var ticket = _service.CreateTicket();
            Tickets.Add(new TicketViewModel(_service, ticket));
        }

        public async Task Initialise()
        {
            foreach (var ticket in await _service.GetTicketsAsync())
            {
                Tickets.Add(new TicketViewModel(_service, ticket));
            }
        }

        public ObservableCollection<TicketViewModel> Tickets { get; } = new ObservableCollection<TicketViewModel>();

        public DelegateCommand CreateTicket { get; }

        public TicketViewModel SelectedTicket { get => selectedTicket; set => SetProperty(ref selectedTicket, value); }


    }
}
