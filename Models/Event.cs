using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PushEventClient.Model;
namespace DataDistributor
{
    /// <summary>
    /// Do not change namespace, as designed for the Datafordeler, odata protocol
    /// It has to be the excact name, or else the odata.entitytype will not be correct(Is it possible to override this?)
    /// </summary>
    public class Event : IModel<int>
    {
        public int Id { get; set; }

        public string Format { get; set; }

        public string Body { get; set; }
    }
}
