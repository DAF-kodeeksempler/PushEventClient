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
        [Route("list")]
        public DAFEventCollection GetList(int pagesize = 100, int pagenumber = 0)
        {
            var data = _db.DAFEvent.Include(e => e.DAFEventHistory)
                .AsNoTrackingWithIdentityResolution()
                .OrderBy(e => e.Id)
                .Reverse()
                .Skip(pagesize*pagenumber)
                .Take(pagesize)
                .ToList();
            var ret = new DAFEventCollection();
            ret.DAFEvents = data;
            ret.pagesize = pagesize;
            ret.pagenumber = pagenumber;
            ret.total = _db.DAFEvent.Count();
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
