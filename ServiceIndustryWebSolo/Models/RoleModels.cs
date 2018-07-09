using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceIndustryWebSolo.Models
{
    public class EditRoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CreateRoleModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UserRoleViewModel
    {
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}