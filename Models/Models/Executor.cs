using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Executor
    {
        public string ExecutorId { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public double Price { get; set; }
        [JsonIgnore]
        public virtual ServiceIndustry ServiceIndustry { get; set; }
        public string ServiceIndustryId { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
        public string CustomerId { get; set; }

        public Executor() { }
        public Executor(string eid, string sid, string cid, double price, DateTime time)
        {
            ExecutorId = eid;
            ServiceIndustryId = sid;
            CustomerId = cid;
            Price = price;
            CreationTime = time;
        }

        public override string ToString()
        {
            return $"ExecutorId:{ExecutorId}; ServiceId:{ServiceIndustryId}; CustomerId:{CustomerId}; Price:{Price}; CreationTime:{CreationTime.ToString("dd-mm-yyyy")}";
        }
    }
}
