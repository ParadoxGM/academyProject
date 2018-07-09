using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class ServiceIndustryType
    {
        public string ServiceIndustryTypeId { get; set; }
        [Display(Name = "Service Industry Type")]
        public string Name { get; set; }
        public string Photo { get; set; }

        [JsonIgnore]
        public virtual ICollection<ServiceIndustry> ServiceIndustrys { get; set; }

        public ServiceIndustryType()
        {
            ServiceIndustrys = new HashSet<ServiceIndustry>();
        }

        public ServiceIndustryType(string id, string name, string photo)
        {
            ServiceIndustryTypeId = id;
            Name = name;
            Photo = photo;
        }

        public override string ToString()
        {
            return $"Id:{ServiceIndustryTypeId};  Name:{Name}";
        }
    }
}
