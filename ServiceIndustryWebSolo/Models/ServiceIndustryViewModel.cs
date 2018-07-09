using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceIndustryWebSolo.Models
{
    public class ServiceIndustryViewModel
    {
        public string ServiceIndustryTypeId { get; set; }
        public List<ServiceIndustryType> ServiceIndustryTypes { get; set; }
        public ServiceIndustryViewModel() { ServiceIndustryTypes = new List<ServiceIndustryType>(); }
    }
}