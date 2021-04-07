using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PushEventClient.Database;
using PushEventClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace PushEventClient.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DAFEventController : ControllerBase
    {
        private readonly DomainModel _db;

        private readonly ILogger<DAFEventController> _logger;

        public DAFEventController(ILogger<DAFEventController> logger, DomainModel db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IEnumerable<DAFEvent> Get()
        {
            var ret = _db.DAFEvent.Include(e => e.DAFEventHistory).AsNoTrackingWithIdentityResolution().ToList();
            return ret;
        }
        [HttpGet]
        [Route("{id}")]
        public DAFEvent Get(int id)
        {
            var ret = _db.DAFEvent.Where(e=>e.Id == id).Include(e => e.DAFEventHistory).AsNoTrackingWithIdentityResolution().First();
            return ret;
        }
    }
}
