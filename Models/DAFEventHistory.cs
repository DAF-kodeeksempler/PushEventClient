using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PushEventClient.Model;
using System;
using DataDistributor;
using Microsoft.EntityFrameworkCore;

namespace PushEventClient.Model
{
    /// <summary>
    /// There may be cases where the events are sent again. We therefore have this secondary model,
    /// that handles this case. In most cases there will be a one -> one relation between a
    /// DAFEvent and DAFEventHistory, with the exception that both XML and JSON is returned.
    /// </summary>
    [Table("DAFEventHistory")]
    public class DAFEventHistory : IModel<int>
    {
        // To distinguish between the different event types. This could be extended to also contain
        // PULL messages if needed.
        public enum EventAction
        {
            PUSH_CREATE,
            PUSH_RESEND
        }

        public enum EventFormat
        {
            XML,
            JSON,
            UNDEFINED
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public DAFEvent DAFEvent { get; set; }
        public string RawBody { get; set; }
        public string RawFormat { get; set; }
        public string OdataMetadata { get; set; } // Additional information form the event, sent in the name, see Startup.cs:92
        public EventFormat Format { get; set; }
        public EventAction Action { get; set; }
        public string IP { get; set; }
    }
}
