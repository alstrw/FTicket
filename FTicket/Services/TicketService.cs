using FTicket.Interfaces;
using FTicket.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FTicket.Services
{
    public class TicketService : ITicketService
    {
        public async Task<IEnumerable<Ticket>> GetTicketsAsync()
        {
            try
            {
                var storageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=fiantest;AccountKey=VlKHocXZLL+0IQ/QNnQEny/KvaO5HsfSYlDPdsA0+yGUH0LPiiCURqN56lsAXMXNKdd3OoOFqIThHBxO7zyusA==;EndpointSuffix=core.windows.net");

                var tableClient = storageAccount.CreateCloudTableClient();

                var ticketsTable = tableClient.GetTableReference("Tickets");

                var ticketEntities = ticketsTable.CreateQuery<DynamicTableEntity>().ToList();

                await Task.CompletedTask;

                return ticketEntities.Select(te =>
                    new Ticket(te.RowKey, te.Properties["CreatedDate"].DateTime, te.Properties["TicketStatus"].StringValue)
                    {
                        Holder = te.PartitionKey,
                        Subject = te.Properties["Subject"].StringValue,

                    }).ToList();

            }
            catch (Exception e)
            {
            }

            return Enumerable.Empty<Ticket>();
        }

        public Ticket CreateTicket()
        {
            return new Ticket();
        }

        public async Task<bool> SaveTicketAsync(Ticket ticket)
        {
            var storageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=fiantest;AccountKey=VlKHocXZLL+0IQ/QNnQEny/KvaO5HsfSYlDPdsA0+yGUH0LPiiCURqN56lsAXMXNKdd3OoOFqIThHBxO7zyusA==;EndpointSuffix=core.windows.net");

            var tableClient = storageAccount.CreateCloudTableClient();

            var ticketsTable = tableClient.GetTableReference("Tickets");

            var ticketEntity = new DynamicTableEntity();

            ticketEntity.PartitionKey = ticket.Holder;
            ticketEntity.RowKey = ticket.TicketKey;
            ticketEntity.Properties["CreatedDate"] = new EntityProperty(ticket.CreatedDateTime);
            ticketEntity.Properties["TicketStatus"] = new EntityProperty(ticket.TicketStatus);
            ticketEntity.Properties["Subject"] = new EntityProperty(ticket.Subject);

            var operation = TableOperation.InsertOrMerge(ticketEntity);

           var result = await ticketsTable.ExecuteAsync(operation);

            return result.HttpStatusCode == (int)HttpStatusCode.OK;

        }

    }
}
