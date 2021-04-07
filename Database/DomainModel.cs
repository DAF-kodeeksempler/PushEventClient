
// using System.Data.Entity;
using System.Reflection;
using PushEventClient.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace PushEventClient.Database
{
    public class DomainModel : DbContext
    {
        public virtual DbSet<DAFEvent> DAFEvent { get; set; }

        public virtual DbSet<DAFEventHistory> DAFEventHistory { get; set; }

        public DomainModel(DbContextOptions<DomainModel> options) : base(options)
        {
            // Generate the database, and database file if not exists.
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        // Detach a local entity. Makes it easier to do ID reference updates
        public void DetachLocal<T, Q>(T t, Q entryId) where T : class, IModel<Q>
        {

            var local = this.Set<T>().Local.FirstOrDefault(entry => entry.Id.Equals(entryId));
            if (!(local == null))
            {
                this.Entry(local).State = EntityState.Detached;
            }
            this.Entry(t).State = EntityState.Modified;
        }
    }
}
