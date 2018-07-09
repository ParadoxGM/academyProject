using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Models.Models
{
    public class ServiceIndustry
    {
        public string ServiceIndustryId { get; set; }
        [Display(Name = "Service Industry")]
        public string Name { get; set; }
        [JsonIgnore]
        [Display(Name = "Service Industry Type")]
        public virtual ServiceIndustryType serviceIndustryType { get; set; }
        [Display(Name = "Service Industry Type Id")]
        public string ServiceIndustryTypeId { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Executor> Executors { get; set; }
        
        public ServiceIndustry()
        {
            Executors = new HashSet<Executor>();
        }

        public ServiceIndustry(string id, string name, string servTypeId, string description)
        {
            ServiceIndustryId = id;
            Name = name;
            ServiceIndustryTypeId = servTypeId;
            Description = description;
        }

        public override string ToString()
        {
            return $"ServiceIndustry Id: {ServiceIndustryId};\n\tServiceIndustryType Id Id: {ServiceIndustryTypeId};\n\tServise Name: {Name};\n\tDescription: {Description} ";
        }
    }
}
