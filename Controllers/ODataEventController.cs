using System;
using System.Linq;
using System.Threading.Tasks;
using PushEventClient.Database;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using DataDistributor;
using PushEventClient.Model;
using static PushEventClient.Model.DAFEventHistory;
using Microsoft.Extensions.Logging;

namespace PushEventClient.Controllers
{
    [ODataRoutePrefix("Events")]
    public class ODataEventController : ODataController
    {
        private readonly DomainModel _db;
        private readonly ILogger<ODataEventController> _logger;

        public ODataEventController(ILogger<ODataEventController> logger, DomainModel db)
        {
            _logger = logger;
            _db = db;
        }

        [ODataRoute()]
        public async Task<IActionResult> Post([FromBody] Event entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // If this is an override of the event, we handle it like that
            var DAFEvent = _db.DAFEvent.Where(s => s.EventID == entity.Id).FirstOrDefault();
            EventAction eventAction;

            if (DAFEvent == null)
            {
                DAFEvent = new DAFEvent
                {
                    EventID = entity.Id
                };
                eventAction = EventAction.PUSH_CREATE;
                await _db.DAFEvent.AddAsync(DAFEvent);
            }
            else
            {
                eventAction = EventAction.PUSH_RESEND;
            }

            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

            var DAFEventHistory = new DAFEventHistory()
            {
                DAFEvent = DAFEvent,
                Action = eventAction,
                RawBody = entity.Body,
                RawFormat = entity.Format,
                Time = DateTime.Now,
                IP = ip
            };
            await _db.DAFEventHistory.AddAsync(DAFEventHistory);

            await _db.SaveChangesAsync();
            return Created(entity);
        }
    }
}
