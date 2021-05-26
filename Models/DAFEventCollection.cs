using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PushEventClient.Model;
using System;
using DataDistributor;
using System.Collections.Generic;

namespace PushEventClient.Model
{
    /// <summary>
    /// Contains a collection of DAFevents, used for pagination results
    /// </summary>
    public class DAFEventCollection
    {
        public int total { get; set; }
        public int pagesize { get; set; }
        public int pagenumber { get; set; }
        public ICollection<DAFEvent> DAFEvents { get; set; }
    }
}