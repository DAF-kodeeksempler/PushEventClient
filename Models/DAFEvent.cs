using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PushEventClient.Model;
using System;
using DataDistributor;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PushEventClient.Model
{
    /// <summary>
    /// Containing the eventID, and a one to many to the history of the event.
    /// See DAFEventHistory for an explanation why.
    /// </summary>
    [Table("DAFEvent")]
    public class DAFEvent : IModel<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EventID { get; set; }
        public ICollection<DAFEventHistory> DAFEventHistory { get; set; }
    }
}